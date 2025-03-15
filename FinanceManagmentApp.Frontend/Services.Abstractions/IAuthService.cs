using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Frontend.Services.Abstractions
{
    public interface IAuthService
    {
        Task<string> GetAccessTokenAsync();
        Task<bool> LoginAsync(UserLoginDTO userLogin);
        Task LogoutAsync();
        Task<string> RefreshTokenAsync();
        Task<bool> RegisterAsync(UserRegisterDTO userRegister);
    }
}