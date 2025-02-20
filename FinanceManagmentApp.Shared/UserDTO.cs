using System.ComponentModel.DataAnnotations;

namespace FinanceManagmentApp.Shared
{
    public sealed class UserDTO
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Length(64, 64)]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        [MaxLength(150)]
        public string DisplayName { get; set; } = string.Empty;
        public IEnumerable<FinancialOperationDTO> FinancialOperations { get; set; } = new List<FinancialOperationDTO>();
    }
}
