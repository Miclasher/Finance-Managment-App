using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Services.Abstractions
{
    public interface ITransactionTypeService
    {
        Task<TransactionTypeDTO> GetByIdAsync(Guid userId, Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TransactionTypeDTO>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid userId, Guid targetId, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid userId, TransactionTypeForUpdateDTO transType, CancellationToken cancellationToken = default);
        Task CreateAsync(Guid userId, TransactionTypeForCreateDTO transType, CancellationToken cancellationToken = default);
    }
}
