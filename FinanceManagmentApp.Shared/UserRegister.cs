using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagmentApp.Shared
{
    public sealed class UserRegister
    {
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
