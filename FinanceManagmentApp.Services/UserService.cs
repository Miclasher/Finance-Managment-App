using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Domain.Repositories;
using FinanceManagmentApp.Services.Abstractions;
using FinanceManagmentApp.Services.Utilities;
using FinanceManagmentApp.Shared;
using System.Security.Cryptography;

namespace FinanceManagmentApp.Services
{
    public sealed class UserService : IUserService
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IRepositoryManager _repositoryManager;
        public UserService(IJwtProvider jwtProvider, IRepositoryManager repositoryManager)
        {
            _jwtProvider = jwtProvider;
            _repositoryManager = repositoryManager;
        }

        public async Task<AuthResponseDTO> LoginAsync(UserLoginDTO user, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));

            var userToLogin = await _repositoryManager.User.GetByUsername(user.Username, cancellationToken);

            if (userToLogin is null || !HashUtility.VerifyPassword(user.Password, userToLogin.PasswordHash, userToLogin.Salt))
            {
                throw new InvalidDataException("Invalid credentials");
            }

            var refreshToken = GenerateJwtTokenEntity(userToLogin.Id);

            await _repositoryManager.RefreshToken.ReplaceUserToken(userToLogin.Id, refreshToken, cancellationToken);

            return new AuthResponseDTO
            {
                AccessToken = _jwtProvider.GenerateAccessToken(userToLogin.Id, userToLogin.Roles.Select(e => e.Name)),
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<AuthResponseDTO> RegisterAsync(UserRegisterDTO newUser, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(newUser, nameof(newUser));

            await ValidateUserCredentials(newUser, cancellationToken);

            User userToAdd = CreateNewUserEntity(newUser);

            var refreshToken = GenerateJwtTokenEntity(userToAdd.Id);

            await _repositoryManager.User.AddAsync(userToAdd, cancellationToken);

            await _repositoryManager.RefreshToken.ReplaceUserToken(userToAdd.Id, refreshToken, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new AuthResponseDTO
            {
                RefreshToken = refreshToken.Token,
                AccessToken = _jwtProvider.GenerateAccessToken(userToAdd.Id, [])
            };
        }

        private static User CreateNewUserEntity(UserRegisterDTO newUser)
        {
            var saltBytes = RandomNumberGenerator.GetBytes(16);

            var hashedPassword = HashUtility.HashPassword(newUser.PlainPassword, saltBytes);

            var userToAdd = new User
            {
                Id = Guid.NewGuid(),
                Username = newUser.Username,
                PasswordHash = hashedPassword,
                Salt = Convert.ToBase64String(saltBytes),
                Email = newUser.Email,
                DisplayName = newUser.DisplayName
            };
            return userToAdd;
        }

        private async Task ValidateUserCredentials(UserRegisterDTO newUser, CancellationToken cancellationToken)
        {
            if (await _repositoryManager.User.UsernameExists(newUser.Username, cancellationToken))
            {
                throw new InvalidOperationException("Username is already taken");
            }
        }

        private JwtRefreshToken GenerateJwtTokenEntity(Guid userId)
        {
            return new JwtRefreshToken
            {
                Id = Guid.NewGuid(),
                Token = _jwtProvider.GenerateRefreshToken(),
                ExpiryDate = DateTime.UtcNow.AddDays(15),
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };
        }
    }
}
