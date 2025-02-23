using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Domain.Repositories;
using FinanceManagmentApp.Services.Abstractions;
using FinanceManagmentApp.Shared;
using Mapster;
using System.Security.Claims;

namespace FinanceManagmentApp.Services
{
    public sealed class FinancialOperationService : IFinancialOperationService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IJwtUtility _jwtUtility;

        public FinancialOperationService(IRepositoryManager repositoryManager, IJwtUtility jwtUtility)
        {
            _jwtUtility = jwtUtility ?? throw new ArgumentNullException(nameof(jwtUtility));
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
        }

        public async Task CreateAsync(ClaimsPrincipal user, FinancialOperationForCreateDTO finOp, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(finOp);
            ArgumentNullException.ThrowIfNull(user);

            var userId = _jwtUtility.GetUserIdFromJwt(user);

            var transactionTypes = await _repositoryManager.TransactionType.GetAllByUserAsync(userId, cancellationToken);
            if (!transactionTypes.Any())
            {
                throw new InvalidOperationException("No transaction types available for the user. Cannot create financial operation.");
            }

            var newFinOp = finOp.Adapt<FinancialOperation>();

            newFinOp.UserId = userId;

            await _repositoryManager.FinancialOperation.AddAsync(newFinOp, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(ClaimsPrincipal user, Guid targetId, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(user);
            var userId = _jwtUtility.GetUserIdFromJwt(user);

            var target = await _repositoryManager.FinancialOperation.GetByIdAsync(targetId, cancellationToken)
                ?? throw new KeyNotFoundException($"Financial operation with id {targetId} was not found in database.");

            if (target.UserId != userId)
            {
                throw new AccessViolationException("Financial operation is owned by another user. Access denied.");
            }

            _repositoryManager.FinancialOperation.Remove(target, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<FinancialOperationDTO>> GetAllAsync(ClaimsPrincipal user, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(user);

            var userId = _jwtUtility.GetUserIdFromJwt(user);

            var finOps = await _repositoryManager.FinancialOperation.GetAllByUserAsync(userId, cancellationToken);

            return finOps.Adapt<IEnumerable<FinancialOperationDTO>>();
        }

        public async Task<FinancialOperationDTO> GetByIdAsync(ClaimsPrincipal user, Guid id, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(user);

            var userId = _jwtUtility.GetUserIdFromJwt(user);

            var finOp = await _repositoryManager.FinancialOperation.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException($"Financial operation with id {id} was not found in database.");

            if (finOp.UserId != userId)
            {
                throw new AccessViolationException("Financial operation is owned by another user. Access denied.");
            }

            return finOp.Adapt<FinancialOperationDTO>();
        }

        public async Task UpdateAsync(ClaimsPrincipal user, FinancialOperationForUpdateAndSummaryDTO finOp, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(user);
            ArgumentNullException.ThrowIfNull(finOp);

            var userId = _jwtUtility.GetUserIdFromJwt(user);

            var finOpToUpdate = await _repositoryManager.FinancialOperation.GetByIdAsync(finOp.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Financial operation with id {finOp.Id} was not found in database.");

            if (finOpToUpdate.UserId != userId)
            {
                throw new AccessViolationException("Financial operation is owned by another user. Access denied.");
            }

            finOpToUpdate.Date = finOp.Date;
            finOpToUpdate.Amount = finOp.Amount;
            finOpToUpdate.TransactionTypeId = finOp.TransactionTypeId;
            finOpToUpdate.UserComment = finOp.UserComment;

            _repositoryManager.FinancialOperation.Update(finOpToUpdate, cancellationToken);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
