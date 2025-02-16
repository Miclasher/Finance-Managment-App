using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Domain.Repositories;
using FinanceManagmentApp.Shared;
using Mapster;

namespace FinanceManagmentApp.Services
{
    public sealed class FinancialOperationService
    {
        private readonly IRepositoryManager _repositoryManager;

        public FinancialOperationService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
        }

        public async Task CreateAsync(FinancialOperationForCreateDTO finOp, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(finOp);

            var newFinOp = finOp.Adapt<FinancialOperation>();

            await _repositoryManager.FinancialOperation.AddAsync(newFinOp, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid targetId, CancellationToken cancellationToken = default)
        {
            var target = await _repositoryManager.FinancialOperation.GetByIdAsync(targetId, cancellationToken)
                ?? throw new KeyNotFoundException($"Financial operation with id {targetId} was not found in database.");

            _repositoryManager.FinancialOperation.Remove(target, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<FinancialOperationDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var finOps = await _repositoryManager.FinancialOperation.GetAllAsync(cancellationToken);

            return finOps.Adapt<IEnumerable<FinancialOperationDTO>>();
        }

        public async Task<FinancialOperationDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var finOp = await _repositoryManager.FinancialOperation.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException($"Financial operation with id {id} was not found in database.");

            return finOp.Adapt<FinancialOperationDTO>();
        }

        public async Task UpdateAsync(FinancialOperationForUpdateDTO finOp, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(finOp);

            var finOpToUpdate = await _repositoryManager.FinancialOperation.GetByIdAsync(finOp.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Financial operation with id {finOp.Id} was not found in database.");

            finOpToUpdate.Date = finOp.Date;
            finOpToUpdate.Amount = finOp.Amount;
            finOpToUpdate.TransactionTypeId = finOp.TransactionTypeId;
            finOpToUpdate.UserComment = finOp.UserComment;

            _repositoryManager.FinancialOperation.Update(finOpToUpdate, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
