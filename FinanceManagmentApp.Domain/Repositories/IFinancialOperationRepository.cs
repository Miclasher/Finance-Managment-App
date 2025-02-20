using FinanceManagmentApp.Domain.Entities;

namespace FinanceManagmentApp.Domain.Repositories
{
    public interface IFinancialOperationRepository : IRepository<FinancialOperation>
    {
        Task<IEnumerable<FinancialOperation>> GetAllByUser(Guid userId, CancellationToken cancellationToken = default);
    }
}
