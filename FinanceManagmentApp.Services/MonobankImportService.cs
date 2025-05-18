using FinanceManagmentApp.Domain.Repositories;
using FinanceManagmentApp.ExternalClients.Abstractions;
using FinanceManagmentApp.Services.Abstractions;
using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Services
{
    public sealed class MonobankImportService : IMonobankImportService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMonobankClient _monobankClient;

        public MonobankImportService(IRepositoryManager repositoryManager, IMonobankClient monobankClient)
        {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _monobankClient = monobankClient ?? throw new ArgumentNullException(nameof(monobankClient));
        }

        public async Task<SummaryDTO> ImportFinancialOperations(Guid userId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            var user = await _repositoryManager.User.GetByIdAsync(userId, cancellationToken)
                ?? throw new InvalidDataException("Cannot find user with provided id.");

            if (string.IsNullOrEmpty(user.MonobankAccountId))
            {
                throw new InvalidDataException("User does not have monobank account linked.");
            }

            var mccToTransactionTypeIdDict = await _repositoryManager.TransactionType.GetMccToTransactionTypeIdDictAsync(userId, cancellationToken);

            if (mccToTransactionTypeIdDict is null || mccToTransactionTypeIdDict.Count == 0)
            {
                throw new InvalidDataException("Cannot find TransactionTypes associated with MCCs.");
            }

            var finOps
                = await _monobankClient.FetchFinancialOperationsAsync(user.MonobankAccountId, from, to, userId, mccToTransactionTypeIdDict)
                ?? throw new InvalidDataException("Financial operations cannot be null.");
            await _repositoryManager.FinancialOperation.AddRangeAsync(finOps, cancellationToken);

            return SummaryService.GenerateSummary(finOps);
        }
    }
}
