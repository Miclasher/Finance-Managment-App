using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Domain.Repositories;
using FinanceManagmentApp.Services.Abstractions;
using FinanceManagmentApp.Shared;
using Mapster;
using System.Security.Claims;

namespace FinanceManagmentApp.Services
{
    public sealed class SummaryService : ISummaryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IJwtUtility _jwtUtility;

        public SummaryService(IJwtUtility jwtUtility, IRepositoryManager repositoryManager)
        {
            _jwtUtility = jwtUtility;
            _repositoryManager = repositoryManager;
        }

        public async Task<SummaryDTO> GetDateRangeSummaryAsync(ClaimsPrincipal user, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(user);
            ArgumentNullException.ThrowIfNull(startDate);
            ArgumentNullException.ThrowIfNull(endDate);

            var userId = _jwtUtility.GetUserIdFromJwt(user);

            var financialOperations = await _repositoryManager.FinancialOperation.GetAllByUserAndDateRangeAsync(userId, startDate, endDate, cancellationToken);

            return GenerateSummary(financialOperations);
        }

        public async Task<SummaryDTO> GetDaySummaryAsync(ClaimsPrincipal user, DateOnly date, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(user);
            ArgumentNullException.ThrowIfNull(date);

            var userId = _jwtUtility.GetUserIdFromJwt(user);

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
