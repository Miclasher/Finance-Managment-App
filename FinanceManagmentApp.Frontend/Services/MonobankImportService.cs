using FinanceManagmentApp.Frontend.Services.Abstractions;
using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Frontend.Services;

internal sealed class MonobankImportService : BaseService, IMonobankImportService
{
    public MonobankImportService(IHttpClientFactory httpClientFactory, IAuthService authService) : base(httpClientFactory, authService)
    {
    }

    public async Task<SummaryDTO> ImportFromMonobankAsync(DateRangeDTO dateRange)
    {
        var importedSummary = await SendAsync<SummaryDTO, DateRangeDTO>("api/MonobankImport/", HttpMethod.Post, dateRange);

        return importedSummary ?? throw new InvalidDataException("Failed to import summary from Monobank response.");
    }
}
