using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.ExternalClients.Abstractions;
using System.Net.Http.Json;

namespace FinanceManagmentApp.Infrastructure.ExternalClients.Monobank
{
    public sealed class MonobankClient : IMonobankClient
    {
        private readonly HttpClient _httpClient;

        public MonobankClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<FinancialOperation>> FetchFinancialOperations(string accountId, DateTime from, DateTime to, Guid userId, Dictionary<int, Guid> mccToTransactionTypeId)
        {
            var fromUnix = ((DateTimeOffset)from).ToUnixTimeSeconds();
            var toUnix = ((DateTimeOffset)to).ToUnixTimeSeconds();
            string url = $"https://api.monobank.ua/personal/statement/{accountId}/{fromUnix}/{toUnix}";

            var financialOperations = new List<FinancialOperation>();
            var transactions = await GetTransactions(url);

            if (transactions is not null)
            {
                financialOperations.AddRange(transactions.Select(e => e.ToFinancialOperation(mccToTransactionTypeId, userId)));
            }

            return financialOperations;
        }

        private async Task<IEnumerable<MonobankTransactionResponseDTO>?> GetTransactions(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<IEnumerable<MonobankTransactionResponseDTO>>();
        }
    }
}
