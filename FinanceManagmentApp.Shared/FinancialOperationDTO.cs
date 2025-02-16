using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagmentApp.Shared
{
    public sealed class FinancialOperationDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [MaxLength(1000)]
        public string? UserComment { get; set; }
        [Required]
        public Guid TransactionTypeId { get; set; }
        public TransactionTypeDTO TransactionType { get; set; } = null!;
    }
}
