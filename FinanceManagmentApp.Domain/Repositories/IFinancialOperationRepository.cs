using FinanceManagmentApp.Domain.Entities;

namespace FinanceManagmentApp.Domain.Repositories
{
    public interface IFinancialOperationRepository : IRepository<FinancialOperation>
    {
        Task<IEnumerable<FinancialOperation>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
