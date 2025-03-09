using FinanceManagmentApp.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;

namespace FinanceManagmentApp.Frontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly AuthenticationStateProvider _authStateProvider;

        private const string refreshTokenKey = "refreshToken";
        private const string accessTokenKey = "accessToken";

        public AuthService(IHttpClientFactory httpClientFactory, AuthenticationStateProvider authStateProvider, IJSRuntime jsRuntime)
        {
            _httpClient = httpClientFactory.CreateClient("FinanceManagmentAppAPI");
            _authStateProvider = authStateProvider;
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> LoginAsync(UserLoginDTO userLogin)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", userLogin);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();

            if (authResponse == null)
            {
                return false;
            }

            await SaveTokens(authResponse);
            (_authStateProvider as CustomAuthenticationStateProvider)!.NotifyUserLogin(authResponse.AccessToken);
            return true;
        }

        public async Task<bool> RegisterAsync(UserRegisterDTO userRegister)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/register", userRegister);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();

            if (authResponse is null)
            {
                return false;
            }

            await SaveTokens(authResponse);
            (_authStateProvider as CustomAuthenticationStateProvider)!.NotifyUserLogin(authResponse.AccessToken);

            return true;
        }

        public async Task<string> RefreshTokenAsync()
        {
            var refreshToken = await GetRefreshToken();

            if (string.IsNullOrEmpty(refreshToken))
            {
                await LogoutAsync();
                return string.Empty;
            }

            var response = await _httpClient.PostAsJsonAsync("api/Auth/loginWithRefreshToken", refreshToken);

            if (!response.IsSuccessStatusCode)
            {
                await LogoutAsync();
                return string.Empty;
            }

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();

            if (authResponse is null)
            {
                await LogoutAsync();
                return string.Empty;
            }

            await SaveTokens(authResponse);
            (_authStateProvider as CustomAuthenticationStateProvider)!.NotifyUserLogin(authResponse.AccessToken);

            return authResponse.AccessToken;
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", refreshTokenKey);
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", accessTokenKey);
            (_authStateProvider as CustomAuthenticationStateProvider)!.NotifyUserLogout();
        }

        public async Task<string> GetAccessToke()
        {
            var accessToken = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", accessTokenKey);

            if (string.IsNullOrEmpty(accessToken) || IsTokenExpired(accessToken))
            {
                return await RefreshTokenAsync();
            }

            return accessToken;
        }

        private async Task SaveTokens(AuthResponseDTO authResponse)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", accessTokenKey, authResponse.AccessToken);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", refreshTokenKey, authResponse.RefreshToken);
        }

        private static bool IsTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.ValidTo < DateTime.UtcNow;
        }

        private async Task<string> GetRefreshToken()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", refreshTokenKey);
        }
    }
}
