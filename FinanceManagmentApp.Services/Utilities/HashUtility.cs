using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagmentApp.Services.Utilities
{
    internal static class HashUtility
    {
        public static string HashPassword(string plainPassword, byte[] saltBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(plainPassword, saltBytes, 10000, HashAlgorithmName.SHA256);

            return Convert.ToBase64String(pbkdf2.GetBytes(32));
        }

        public static bool VerifyPassword(string plainPassword, string hashedPassword, string salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(plainPassword, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256);

            return Convert.ToBase64String(pbkdf2.GetBytes(32)) == hashedPassword;
        }
    }
}
