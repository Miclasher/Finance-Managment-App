using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Services.Abstractions
{
    public interface ITransactionTypeService
    {
        Task<TransactionTypeDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TransactionTypeDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid targetId, CancellationToken cancellationToken = default);
        Task UpdateAsync(TransactionTypeForUpdateDTO transType, CancellationToken cancellationToken = default);
        Task CreateAsync(TransactionTypeForCreateDTO transTpye, CancellationToken cancellationToken = default);
    }
}
