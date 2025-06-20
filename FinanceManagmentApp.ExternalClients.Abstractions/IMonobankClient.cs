﻿using FinanceManagmentApp.Domain.Entities;

namespace FinanceManagmentApp.ExternalClients.Abstractions
{
    public interface IMonobankClient
    {
        Task<IEnumerable<FinancialOperation>> FetchFinancialOperationsAsync(string accountId, DateTime from, DateTime to, Guid userId, Dictionary<int, Guid> mccToTransactionTypeId);
    }
}
