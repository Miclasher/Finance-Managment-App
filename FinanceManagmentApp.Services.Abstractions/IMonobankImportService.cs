using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Services.Abstractions;

public interface IMonobankImportService
{
    Task<SummaryDTO> ImportFinancialOperations(Guid userId, DateTime from, DateTime to, CancellationToken cancellationToken = default);
}
