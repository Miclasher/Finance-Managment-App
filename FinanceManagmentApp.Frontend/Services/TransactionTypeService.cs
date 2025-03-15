using FinanceManagmentApp.Frontend.Services.Abstractions;
using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Frontend.Services
{
    public class TransactionTypeService : BaseService, ITransactionTypeService
    {
        public TransactionTypeService(IHttpClientFactory httpClientFactory, IAuthService authService) : base(httpClientFactory, authService)
        {
        }

        public async Task CreateAsync(TransactionTypeForCreateDTO transactionType)
        {
            await AddAuthorizationHeaderAsync();
            var response = await _httpClient.PostAsJsonAsync("api/TransactionType", transactionType);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            await AddAuthorizationHeaderAsync();
            var response = await _httpClient.DeleteAsync($"api/TransactionType/{id}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<TransactionTypeDTO>> GetAllAsync()
        {
            await AddAuthorizationHeaderAsync();
            var response = await _httpClient.GetAsync("api/TransactionType");

            response.EnsureSuccessStatusCode();

            var transactionTypes = await response.Content.ReadFromJsonAsync<List<TransactionTypeDTO>>();

            return transactionTypes! ?? throw new InvalidDataException("Failed to get transaction types from server response.");
        }

        public async Task<TransactionTypeDTO> GetByIdAsync(Guid id)
        {
            await AddAuthorizationHeaderAsync();
            var response = await _httpClient.GetAsync($"api/TransactionType/{id}");

            response.EnsureSuccessStatusCode();

            var transactionType = await response.Content.ReadFromJsonAsync<TransactionTypeDTO>();

            return transactionType ?? throw new InvalidDataException("Failed to get transaction type from server response.");
        }

        public async Task UpdateAsync(TransactionTypeDTO transactionType)
        {
            await AddAuthorizationHeaderAsync();
            var response = await _httpClient.PutAsJsonAsync("api/TransactionType", transactionType);

            response.EnsureSuccessStatusCode();
        }
    }
}
