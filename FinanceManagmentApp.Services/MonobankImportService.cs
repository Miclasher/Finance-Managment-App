using FinanceManagmentApp.Domain.Repositories;
using FinanceManagmentApp.Services.Abstractions;
using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Services
{
    public sealed class MonobankImportService : IMonobankImportService
    {
        private readonly IRepositoryManager _repositoryManager;

        public MonobankImportService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
        }

        public Task<IEnumerable<FinancialOperationDTO>> ImportFinancialOperations(Guid userId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
