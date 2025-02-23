using FinanceManagmentApp.Shared;
using System.Security.Claims;

namespace FinanceManagmentApp.Services.Abstractions
{
    public interface ISummaryService
    {
        Task<SummaryDTO> GetDaySummaryAsync(ClaimsPrincipal user, DateOnly date, CancellationToken cancellationToken = default);
        Task<SummaryDTO> GetDateRangeSummaryAsync(ClaimsPrincipal user, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default);
    }
}
