﻿using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Domain.Repositories;

namespace FinanceManagmentApp.Infrastructure.Repositories
{
    internal sealed class TransactionTypeRepository : Repository<TransactionType>, ITransactionTypeRepository
    {
        public TransactionTypeRepository(FinanceManagmentAppContext context) : base(context)
        {
        }
    }
}
