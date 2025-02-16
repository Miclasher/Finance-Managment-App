using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public sealed class FinancialOperationForCreateDTO
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [MaxLength(1000)]
        public string? UserComment { get; set; }
        [Required]
        public Guid TransactionTypeId { get; set; }
    }
}
