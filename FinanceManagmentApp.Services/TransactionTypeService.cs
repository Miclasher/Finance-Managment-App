using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Domain.Repositories;
using FinanceManagmentApp.Services.Abstractions;
using FinanceManagmentApp.Shared;
using Mapster;
using System.Security.Claims;

namespace FinanceManagmentApp.Services
{
    public sealed class TransactionTypeService : ITransactionTypeService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IJwtUtility _jwtUtility;

        public TransactionTypeService(IRepositoryManager repositoryManager, IJwtUtility jwtUtility)
        {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _jwtUtility = jwtUtility;
        }

        public async Task CreateAsync(ClaimsPrincipal user, TransactionTypeForCreateDTO transType, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(transType);
            ArgumentNullException.ThrowIfNull(user);

            var newTransType = transType.Adapt<TransactionType>();

            newTransType.UserId = _jwtUtility.GetUserIdFromJwt(user);

            await _repositoryManager.TransactionType.AddAsync(newTransType, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(ClaimsPrincipal user, Guid targetId, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(user);

            var target = await _repositoryManager.TransactionType.GetByIdAsync(targetId, cancellationToken)
                ?? throw new KeyNotFoundException($"Transaction type with id {targetId} was not found in database.");

            if(target.UserId != _jwtUtility.GetUserIdFromJwt(user))
            {
                throw new AccessViolationException("Transaction type is owned by another user. Access denied.");
            }
            if (target.FinancialOperations.Any())
            {
                throw new InvalidOperationException($"Cannot delete transaction type with id {targetId} because it has associated financial operations.");
            }

            _repositoryManager.TransactionType.Remove(target, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<TransactionTypeDTO>> GetAllAsync(ClaimsPrincipal user, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(user);

            var userId = _jwtUtility.GetUserIdFromJwt(user);

            var transTypes = await _repositoryManager.TransactionType.GetAllByUserAsync(userId, cancellationToken);

            return transTypes.Adapt<IEnumerable<TransactionTypeDTO>>();
        }

        public async Task<TransactionTypeDTO> GetByIdAsync(ClaimsPrincipal user, Guid id, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(user);

            var transType = await _repositoryManager.TransactionType.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException($"Transaction type with id {id} was not found in database.");

            if (transType.UserId != _jwtUtility.GetUserIdFromJwt(user))
            {
                throw new AccessViolationException("Transaction type is owned by another user. Access denied.");
            }

            return transType.Adapt<TransactionTypeDTO>();
        }

        public async Task UpdateAsync(ClaimsPrincipal user, TransactionTypeForUpdateDTO transType, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(transType);
            ArgumentNullException.ThrowIfNull(user);

            var transTypeToUpdate = await _repositoryManager.TransactionType.GetByIdAsync(transType.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Transaction type with id {transType.Id} was not found in database.");

            if (transTypeToUpdate.UserId != _jwtUtility.GetUserIdFromJwt(user))
            {
                throw new AccessViolationException("Transaction type is owned by another user. Access denied.");
            }

            transTypeToUpdate.Name = transType.Name;
            transTypeToUpdate.IsExpense = transType.IsExpense;

            _repositoryManager.TransactionType.Update(transTypeToUpdate, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
