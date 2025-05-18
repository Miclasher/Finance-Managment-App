using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Services.Abstractions
{
    public interface IMonobankImportService
    {
        Task<IEnumerable<FinancialOperationDTO>> ImportFinancialOperations(Guid userId, DateTime from, DateTime to, CancellationToken cancellationToken = default);
    }
}
