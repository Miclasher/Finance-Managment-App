using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Frontend.Services.Abstractions
{
    public interface ITransactionTypeService
    {
        Task<IEnumerable<TransactionTypeDTO>> GetAllAsync();
        Task<TransactionTypeDTO> GetByIdAsync(Guid id);
        Task<TransactionTypeDTO> CreateAsync(TransactionTypeForCreateDTO transactionType);
        Task<TransactionTypeDTO> UpdateAsync(TransactionTypeDTO transactionType);
        Task DeleteAsync(Guid id);
    }
}
