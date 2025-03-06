﻿using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Domain.Repositories;
using FinanceManagmentApp.Services.Abstractions;
using FinanceManagmentApp.Shared;
using Mapster;

namespace FinanceManagmentApp.Services
{
    public sealed class SummaryService : ISummaryService
    {
        private readonly IRepositoryManager _repositoryManager;

        public SummaryService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
        }

        public async Task<SummaryDTO> GetDateRangeSummaryAsync(Guid userId, DateRangeDTO dateRange, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(dateRange);

            if (dateRange.StartDate > dateRange.EndDate)
            {
                throw new InvalidDataException("Start date cannot be greater than end date.");
            }

            var financialOperations = await _repositoryManager.FinancialOperation.GetAllByUserAndDateRangeAsync(userId, dateRange.StartDate, dateRange.EndDate, cancellationToken);

            return GenerateSummary(financialOperations);
        }

        public async Task<SummaryDTO> GetDaySummaryAsync(Guid userId, DateOnly date, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(date);

            var financialOperations = await _repositoryManager.FinancialOperation.GetAllByUserAndDateRangeAsync(userId, date, date, cancellationToken);

            return GenerateSummary(financialOperations);
        }

        private static SummaryDTO GenerateSummary(IEnumerable<FinancialOperation> financialOperations)
        {
            var totalIncome = 0m;
            var totalExpense = 0m;

            foreach (var operation in financialOperations)
            {
                if (operation.TransactionType.IsExpense)
                {
                    totalExpense += operation.Amount;
                }
                else
                {
                    totalIncome += operation.Amount;
                }
            }

            return new SummaryDTO
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Operations = financialOperations.Adapt<IEnumerable<FinancialOperationDTO>>()
            };
        }
    }
}
