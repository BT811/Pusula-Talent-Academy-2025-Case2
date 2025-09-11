using System.ComponentModel.DataAnnotations;

namespace StudentAutomation.Frontend.Models
{
    public class UpdateUserModel
    {
        [Required(ErrorMessage = "Ad gerekli")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad gerekli")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email gerekli")]
        [EmailAddress(ErrorMessage = "Geçerli bir email girin")]
        public string Email { get; set; }

        // Opsiyonel şifre
        public string? PasswordHash { get; set; }

         public string Role { get; set; }
    }
}
