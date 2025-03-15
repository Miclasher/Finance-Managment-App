using FinanceManagmentApp.Frontend.Services.Abstractions;
using FinanceManagmentApp.Frontend.Utilities;
using FinanceManagmentApp.Shared;
using System;

namespace FinanceManagmentApp.Frontend.Services
{
    internal sealed class SummaryService : BaseService, ISummaryService
    {
        public SummaryService(IHttpClientFactory httpClientFactory, IAuthService authService) : base(httpClientFactory, authService)
        {
        }

        public async Task<SummaryDTO> GetDateRangeSummary(DateRangeDTO dateRange)
        {
            await AddAuthorizationHeaderAsync();
            var response = await _httpClient.PostAsJsonAsync("api/dateRangeSummary/", dateRange);

            await response.CustomEnsureSuccessStatusCode();

            var summary = await response.Content.ReadFromJsonAsync<SummaryDTO>();

            return summary ?? throw new InvalidDataException("Failed to get summary from server response.");
        }

        public async Task<SummaryDTO> GetDaySummary(DateOnly dateOnly)
        {
            await AddAuthorizationHeaderAsync();
            var response = await _httpClient.PostAsJsonAsync("api/daySummary/", dateOnly);

            await response.CustomEnsureSuccessStatusCode();

            var summary = await response.Content.ReadFromJsonAsync<SummaryDTO>();

            return summary ?? throw new InvalidDataException("Failed to get summary from server response.");
        }
    }
}
