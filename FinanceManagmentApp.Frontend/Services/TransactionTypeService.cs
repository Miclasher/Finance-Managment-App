using FinanceManagmentApp.Frontend.Services.Abstractions;
using FinanceManagmentApp.Shared;
using System.Net.Http.Headers;

namespace FinanceManagmentApp.Frontend.Services
{
    public class TransactionTypeService : BaseService, ITransactionTypeService
    {
        public TransactionTypeService(IHttpClientFactory httpClientFactory, IAuthService authService) : base(httpClientFactory, authService)
        {
        }

        public Task<TransactionTypeDTO> CreateAsync(TransactionTypeForCreateDTO transactionType)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TransactionTypeDTO>> GetAllAsync()
        {
            await AddAuthorizationHeaderAsync();
            var response = await _httpClient.GetAsync("api/TransactionType");

            response.EnsureSuccessStatusCode();

            var transactionTypes = await response.Content.ReadFromJsonAsync<List<TransactionTypeDTO>>();
            return transactionTypes!;
        }

        public Task<TransactionTypeDTO> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionTypeDTO> UpdateAsync(TransactionTypeDTO transactionType)
        {
            throw new NotImplementedException();
        }
    }
}
