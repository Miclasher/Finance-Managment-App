using FinanceManagmentApp.Frontend.Services.Abstractions;
using FinanceManagmentApp.Frontend.Utilities;
using System.Net.Http.Headers;

namespace FinanceManagmentApp.Frontend.Services
{
    internal abstract class BaseService
    {
        private protected readonly HttpClient _httpClient;
        private protected readonly IAuthService _authService;

        protected BaseService(IHttpClientFactory httpClientFactory, IAuthService authService)
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

        private protected async Task<HttpResponseMessage> SendAsync(string url, HttpMethod method)
        {
            return await SendAsync<object>(url, method, null!);
        }

        private protected async Task<HttpResponseMessage> SendAsync<T>(string url, HttpMethod method, T data)
        {
            await AddAuthorizationHeaderAsync();

            var response = await _httpClient.SendAsync(new HttpRequestMessage()
            {
                Method = method,
                RequestUri = new Uri(url, UriKind.Relative),
                Content = data == null ? null : JsonContent.Create(data, mediaType: null)
            });

            await response.CustomEnsureSuccessStatusCode();

            return response;
        }

        private protected async Task<T?> SendAsync<T>(string url, HttpMethod method)
        {
            var response = await SendAsync<T>(url, method, default!);

            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}
