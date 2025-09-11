using System.ComponentModel.DataAnnotations;

namespace StudentAutomation.Frontend.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Ad gerekli")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad gerekli")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email gerekli")]
        [EmailAddress(ErrorMessage = "Geçerli bir email girin")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gerekli")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalı")]
        public string PasswordHash  { get; set; }

        [Required(ErrorMessage = "Rol seçiniz")]
        public string Role { get; set; }
    }
}
