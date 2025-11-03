using FinanceManagmentApp.Domain.Entities;

namespace FinanceManagmentApp.Domain.Repositories;

public interface ITransactionTypeTemplateRepository : IRepository<TransactionTypeTemplate>
{
    Task<IEnumerable<TransactionTypeTemplate>> GetAllWithMccAsync(CancellationToken cancellationToken = default);
}
