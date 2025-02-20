﻿using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Services.Abstractions
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(UserRegisterDTO newUser, CancellationToken cancellationToken = default);
        Task<AuthResponseDTO> LoginAsync(UserLoginDTO user, CancellationToken cancellationToken = default);
    }
}
