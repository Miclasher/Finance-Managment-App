using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Domain.Repositories;
using FinanceManagmentApp.Services.Abstractions;
using FinanceManagmentApp.Services.Utilities;
using FinanceManagmentApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

            var refreshToken = _jwtProvider.GenerateRefreshToken();

            await _repositoryManager.RefreshToken.ReplaceUserToken(userToLogin.Id, refreshToken, cancellationToken);

            return new AuthResponseDTO
            {
                AccessToken = _jwtProvider.GenerateAccessToken(userToLogin.Id, userToLogin.Roles.Select(e => e.Name)),
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthResponseDTO> RegisterAsync(UserRegister newUser, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(newUser, nameof(newUser));

            if (await _repositoryManager.User.UsernameExists(newUser.Username, cancellationToken))
            {
                throw new InvalidOperationException("Username is already taken");
            }

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

            var refreshToken = _jwtProvider.GenerateRefreshToken();

            await _repositoryManager.User.AddAsync(userToAdd, cancellationToken);

            await _repositoryManager.RefreshToken.ReplaceUserToken(userToAdd.Id, refreshToken, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new AuthResponseDTO
            {
                RefreshToken = refreshToken,
                AccessToken = _jwtProvider.GenerateAccessToken(userToAdd.Id, [])
            };
        }

        public Task UpdateAsync(UserForUpdateDTO user, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
