using FinanceManagmentApp.Shared;
using System.Security.Claims;

namespace FinanceManagmentApp.Services.Abstractions
{
    public interface IFinancialOperationService
    {
        Task<FinancialOperationDTO> GetByIdAsync(ClaimsPrincipal user, Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<FinancialOperationDTO>> GetAllAsync(ClaimsPrincipal user, CancellationToken cancellationToken = default);
        Task CreateAsync(ClaimsPrincipal user, FinancialOperationForCreateDTO finOp, CancellationToken cancellationToken = default);
        Task UpdateAsync(ClaimsPrincipal user, FinancialOperationForUpdateAndSummaryDTO finOp, CancellationToken cancellationToken = default);
        Task DeleteAsync(ClaimsPrincipal user, Guid targetId, CancellationToken cancellationToken = default);
    }
}
