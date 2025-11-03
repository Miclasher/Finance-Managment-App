using FinanceManagmentApp.Shared;

namespace FinanceManagmentApp.Frontend.Services.Abstractions;

public interface IFinancialOperationService
{
    Task<IEnumerable<FinancialOperationDTO>> GetAllAsync();
    Task<FinancialOperationDTO> GetByIdAsync(Guid id);
    Task CreateAsync(FinancialOperationForCreateDTO financialOperation);
    Task UpdateAsync(FinancialOperationDTO financialOperation);
    Task DeleteAsync(Guid id);
}
