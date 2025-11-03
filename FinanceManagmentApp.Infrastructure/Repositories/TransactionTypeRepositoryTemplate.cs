using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceManagmentApp.Infrastructure.Repositories;

internal class TransactionTypeRepositoryTemplate : Repository<TransactionTypeTemplate>, ITransactionTypeTemplateRepository
{
    public TransactionTypeRepositoryTemplate(FinanceManagmentAppContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TransactionTypeTemplate>> GetAllWithMccAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.Include(e => e.Mccs).ToListAsync(cancellationToken);
    }
}