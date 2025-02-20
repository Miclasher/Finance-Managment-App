﻿using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceManagmentApp.Infrastructure.Repositories
{
    internal sealed class FinancialOperationRepository : Repository<FinancialOperation>, IFinancialOperationRepository
    {
        public FinancialOperationRepository(FinanceManagmentAppContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<FinancialOperation>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.Include(e => e.TransactionType).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<FinancialOperation>> GetAllByUser(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(e => e.UserId == userId).ToListAsync(cancellationToken);
        }

        public override async Task<FinancialOperation> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return (await _dbSet.Include(e => e.TransactionType).Where(e => e.Id == id).FirstOrDefaultAsync(cancellationToken))!;
        }
    }
}
