﻿using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceManagmentApp.Infrastructure.Repositories
{
    internal sealed class TransactionTypeRepository : Repository<TransactionType>, ITransactionTypeRepository
    {
        public TransactionTypeRepository(FinanceManagmentAppContext context) : base(context)
        {
        }

        public async override Task<TransactionType> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return (await _dbSet.Include(e => e.FinancialOperations).FirstOrDefaultAsync(e => e.Id == id, cancellationToken))!;
        }

        public async Task<IEnumerable<TransactionType>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().Where(e => e.UserId == userId).ToListAsync(cancellationToken);
        }
    }
}
