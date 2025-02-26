using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Services.Abstractions
{
    public interface IFinancialOperationService
    {
        Task<FinancialOperationDTO> GetByIdAsync(Guid userId, Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<FinancialOperationDTO>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);
        Task CreateAsync(Guid userId, FinancialOperationForCreateDTO finOp, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid userId, FinancialOperationForUpdateAndSummaryDTO finOp, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid userId, Guid targetId, CancellationToken cancellationToken = default);
    }
}
