﻿using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Frontend.Services.Abstractions
{
    public interface IAuthService
    {
        Task<string> GetAccessTokenAsync();
        Task LoginAsync(UserLoginDTO userLogin);
        Task LogoutAsync();
        Task<string> RefreshTokenAsync();
        Task RegisterAsync(UserRegisterDTO userRegister);
    }
}