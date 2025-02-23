using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagmentApp.Shared
{
    public sealed class FinancialOperationsSummaryDTO
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public IEnumerable<FinancialOperationDTO> Operations { get; set; } = new List<FinancialOperationDTO>();
    }
}
