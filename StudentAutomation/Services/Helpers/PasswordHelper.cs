using Microsoft.AspNetCore.Identity;

namespace StudentAutomation.Services
{
    public static class PasswordHelper
    {
        private static readonly PasswordHasher<string> hasher = new();

        public static string HashPassword(string password)
        {
            return hasher.HashPassword(null, password);
        }

        public static bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            return hasher.VerifyHashedPassword(null, hashedPassword, providedPassword) != PasswordVerificationResult.Failed;
        }
    }
}
