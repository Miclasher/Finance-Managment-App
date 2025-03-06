using FinanceManagmentApp.Domain.Entities;

namespace FinanceManagmentApp.Domain.Repositories
{
    public interface IFinancialOperationRepository : IRepository<FinancialOperation>
    {
        Task<IEnumerable<FinancialOperation>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<FinancialOperation>> GetAllByUserAndDateRangeAsync(Guid userId, DateOnly from, DateOnly to, CancellationToken cancellationToken = default);
    }
}
