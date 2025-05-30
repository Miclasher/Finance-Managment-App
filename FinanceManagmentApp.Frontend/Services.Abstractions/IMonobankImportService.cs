using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Frontend.Services.Abstractions
{
    public interface IMonobankImportService
    {
        Task<SummaryDTO> ImportFromMonobankAsync(DateRangeDTO dateRange);
    }
}
