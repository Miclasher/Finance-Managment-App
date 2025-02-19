﻿using System.ComponentModel.DataAnnotations;

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
        [Required]
        public Guid UserId { get; set; }
        public UserDTO User { get; set; } = null!;
    }
}
