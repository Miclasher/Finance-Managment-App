using System.ComponentModel.DataAnnotations;

namespace FinanceManagmentApp.Shared
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
        [Required]
        public Guid UserId { get; set; }
    }
}
