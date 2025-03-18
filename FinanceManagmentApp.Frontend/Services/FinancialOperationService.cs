using FinanceManagmentApp.Frontend.Services.Abstractions;
using FinanceManagmentApp.Frontend.Utilities;
using FinanceManagmentApp.Shared;
using System.Runtime.InteropServices;

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

            await SendAsync("api/FinancialOperation", HttpMethod.Post, financialOperation);
        }

        public async Task DeleteAsync(Guid id)
        {
            await SendAsync($"api/FinancialOperation/{id}", HttpMethod.Delete);
        }

        public async Task<IEnumerable<FinancialOperationDTO>> GetAllAsync()
        {
            var response = await SendAsync("api/FinancialOperation", HttpMethod.Get);

            var financialOperations = await response.Content.ReadFromJsonAsync<List<FinancialOperationDTO>>();

            return financialOperations ?? throw new InvalidDataException("Failed to get financial operations from server response.");
        }

        public async Task<FinancialOperationDTO> GetByIdAsync(Guid id)
        {
            var response = await SendAsync($"api/FinancialOperation/{id}", HttpMethod.Get);

            var financialOperation = await response.Content.ReadFromJsonAsync<FinancialOperationDTO>();

            return financialOperation ?? throw new InvalidDataException("Failed to get financial operation from server response.");
        }

        public async Task UpdateAsync(FinancialOperationDTO financialOperation)
        {
            await SendAsync("api/FinancialOperation", HttpMethod.Put, financialOperation);
        }
    }
}
