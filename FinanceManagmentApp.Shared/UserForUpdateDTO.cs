using System.ComponentModel.DataAnnotations;

namespace FinanceManagmentApp.Shared
{
    public sealed class UserForUpdateDTO
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
        public string PlainPassword { get; set; } = string.Empty;
        [Required]
        [MaxLength(150)]
        public string DisplayName { get; set; } = string.Empty;
    }
}
