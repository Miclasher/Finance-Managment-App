using FinanceManagmentApp.Frontend.Services.Abstractions;
using FinanceManagmentApp.Frontend.Utilities;
using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Frontend.Services
{
    internal sealed class FinancialOperationService : BaseService, IFinancialOperationService
    {
        public FinancialOperationService(IHttpClientFactory httpClientFactory, IAuthService authService) : base(httpClientFactory, authService)
        {
        }

        public async Task CreateAsync(FinancialOperationForCreateDTO financialOperation)
        {
            ArgumentNullException.ThrowIfNull(financialOperation);

            await AddAuthorizationHeaderAsync();
            var response = await _httpClient.PostAsJsonAsync("api/FinancialOperation", financialOperation);

            await response.CustomEnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            await AddAuthorizationHeaderAsync();
            var response = await _httpClient.DeleteAsync($"api/FinancialOperation/{id}");

            await response.CustomEnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<FinancialOperationDTO>> GetAllAsync()
        {
            await AddAuthorizationHeaderAsync();
            var response = await _httpClient.GetAsync("api/FinancialOperation");

            await response.CustomEnsureSuccessStatusCode();

            var financialOperations = await response.Content.ReadFromJsonAsync<List<FinancialOperationDTO>>();

            return financialOperations ?? throw new InvalidDataException("Failed to get financial operations from server response.");
        }

        public async Task<FinancialOperationDTO> GetByIdAsync(Guid id)
        {
            await AddAuthorizationHeaderAsync();
            var response = await _httpClient.GetAsync($"api/FinancialOperation/{id}");

            await response.CustomEnsureSuccessStatusCode();

            var financialOperation = await response.Content.ReadFromJsonAsync<FinancialOperationDTO>();

            return financialOperation ?? throw new InvalidDataException("Failed to get financial operation from server response.");
        }

        public async Task UpdateAsync(FinancialOperationDTO financialOperation)
        {
            await AddAuthorizationHeaderAsync();
            var response = await _httpClient.PutAsJsonAsync("api/FinancialOperation", financialOperation);

            await response.CustomEnsureSuccessStatusCode();
        }
    }
}
