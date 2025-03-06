using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Services.Abstractions
{
    public interface ISummaryService
    {
        Task<SummaryDTO> GetDaySummaryAsync(Guid userId, DateOnly date, CancellationToken cancellationToken = default);
        Task<SummaryDTO> GetDateRangeSummaryAsync(Guid userId, DateRangeDTO dateRange, CancellationToken cancellationToken = default);
    }
}
