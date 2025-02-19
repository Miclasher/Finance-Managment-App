using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Services.Abstractions
{
    public interface IUserService
    {
        Task<AuthResponseDTO> RegisterAsync(UserRegister newUser, CancellationToken cancellationToken = default);
        Task UpdateAsync(UserForUpdateDTO user, CancellationToken cancellationToken = default);
        Task<AuthResponseDTO> LoginAsync(UserLoginDTO user, CancellationToken cancellationToken = default);
    }
}
