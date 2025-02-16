using FinanceManagmentApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagmentApp.Services.Abstractions
{
    public interface IFinancialOperationService
    {
        Task<FinancialOperationDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<FinancialOperationDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task CreateAsync(FinancialOperationForCreateDTO finOp, CancellationToken cancellationToken = default);
        Task UpdateAsync(FinancialOperationForUpdateDTO finOp, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid targetId, CancellationToken cancellationToken = default);
    }
}
