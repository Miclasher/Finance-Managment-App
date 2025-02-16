using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Domain.Repositories;
using FinanceManagmentApp.Services.Abstractions;
using FinanceManagmentApp.Shared;
using Mapster;

namespace FinanceManagmentApp.Services
{
    public sealed class TransactionTypeService : ITransactionTypeService
    {
        private readonly IRepositoryManager _repositoryManager;

        public TransactionTypeService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
        }

        public async Task CreateAsync(TransactionTypeForCreateDTO transType, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(transType);

            var newTransType = transType.Adapt<TransactionType>();

            await _repositoryManager.TransactionType.AddAsync(newTransType, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid targetId, CancellationToken cancellationToken = default)
        {
            var target = await _repositoryManager.TransactionType.GetByIdAsync(targetId, cancellationToken)
                ?? throw new KeyNotFoundException($"Transaction type with id {targetId} was not found in database.");

            if (target.FinancialOperations.Any())
            {
                throw new InvalidOperationException($"Cannot delete transaction type with id {targetId} because it has associated financial operations.");
            }

            _repositoryManager.TransactionType.Remove(target, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<TransactionTypeDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var transTypes = await _repositoryManager.TransactionType.GetAllAsync(cancellationToken);

            return transTypes.Adapt<IEnumerable<TransactionTypeDTO>>();
        }

        public async Task<TransactionTypeDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var transType = await _repositoryManager.TransactionType.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException($"Transaction type with id {id} was not found in database.");

            return transType.Adapt<TransactionTypeDTO>();
        }

        public async Task UpdateAsync(TransactionTypeForUpdateDTO transType, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(transType);

            var transTypeToUpdate = await _repositoryManager.TransactionType.GetByIdAsync(transType.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Transaction type with id {transType.Id} was not found in database.");

            transTypeToUpdate.Name = transType.Name;
            transTypeToUpdate.IsExpense = transType.IsExpense;

            _repositoryManager.TransactionType.Update(transTypeToUpdate, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
