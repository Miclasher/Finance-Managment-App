using System.Net.Http.Headers;
using FinanceManagmentApp.Frontend.Services.Abstractions;

namespace FinanceManagmentApp.Frontend.Services
{
    public abstract class BaseService
    {
        private protected readonly HttpClient _httpClient;
        private protected readonly IAuthService _authService;

        public BaseService(IHttpClientFactory httpClientFactory, IAuthService authService)
        {
            _httpClient = httpClientFactory.CreateClient("FinanceManagmentAppAPI");
            _authService = authService;
        }

        private protected async Task AddAuthorizationHeaderAsync()
        {
            var token = await _authService.GetAccessTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
