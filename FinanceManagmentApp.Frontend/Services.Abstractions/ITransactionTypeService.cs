using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Frontend.Services.Abstractions;

public interface ITransactionTypeService
{
    Task<IEnumerable<TransactionTypeDTO>> GetAllAsync();
    Task<TransactionTypeDTO> GetByIdAsync(Guid id);
    Task CreateAsync(TransactionTypeForCreateDTO transactionType);
    Task UpdateAsync(TransactionTypeDTO transactionType);
    Task DeleteAsync(Guid id);
}
