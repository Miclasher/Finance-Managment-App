using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.ExternalClients.Abstractions;
using System.Net.Http.Json;

namespace FinanceManagmentApp.Infrastructure.ExternalClients.Monobank
{
    public sealed class MonobankClient : IMonobankClient
    {
        private readonly HttpClient _httpClient;

        public MonobankClient(IHttpClientFactory httpClientFactory)
        {
            ArgumentNullException.ThrowIfNull(httpClientFactory);

            _httpClient = httpClientFactory.CreateClient("MonobankAPI");
        }

        public async Task<IEnumerable<FinancialOperation>> FetchFinancialOperationsAsync(string monobankAPIToken, DateTime from, DateTime to, Guid userId, Dictionary<int, Guid> mccToTransactionTypeId)
        {
            var fromUnix = ((DateTimeOffset)from).ToUnixTimeSeconds();
            var toUnix = ((DateTimeOffset)to).ToUnixTimeSeconds();

            // Monobank will respond with code 400 if the time frame is greater than 31 days + 1 hour (2,682,000 seconds)
            if (toUnix - fromUnix >= 2682000)
            {
                throw new ArgumentException("Time frame for transaction import shouldn't be longer than a month.");
            }

            string uri = $"/personal/statement/0/{fromUnix}/{toUnix}";
            _httpClient.DefaultRequestHeaders.Add("X-Token", monobankAPIToken);

            var financialOperations = new List<FinancialOperation>();
            var transactions = await GetTransactions(uri);

            if (transactions is not null)
            {
                financialOperations.AddRange(transactions.Select(e => e.ToFinancialOperation(mccToTransactionTypeId, userId)));
            }

            return financialOperations;
        }

        private async Task<IEnumerable<MonobankTransactionResponseDTO>?> GetTransactions(string uri)
        {
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<IEnumerable<MonobankTransactionResponseDTO>>();
        }
    }
}
