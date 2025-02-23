using FinanceManagmentApp.Shared;
using System.Security.Claims;

namespace FinanceManagmentApp.Services.Abstractions
{
    public interface ITransactionTypeService
    {
        Task<TransactionTypeDTO> GetByIdAsync(ClaimsPrincipal user, Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TransactionTypeDTO>> GetAllAsync(ClaimsPrincipal user, CancellationToken cancellationToken = default);
        Task DeleteAsync(ClaimsPrincipal user, Guid targetId, CancellationToken cancellationToken = default);
        Task UpdateAsync(ClaimsPrincipal user, TransactionTypeForUpdateDTO transType, CancellationToken cancellationToken = default);
        Task CreateAsync(ClaimsPrincipal user, TransactionTypeForCreateDTO transType, CancellationToken cancellationToken = default);
    }
}
