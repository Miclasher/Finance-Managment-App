using FinanceManagmentApp.Domain.Entities;

namespace FinanceManagmentApp.Domain.Repositories
{
    public interface ITransactionTypeRepository : IRepository<TransactionType>
    {
        Task<IEnumerable<TransactionType>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
