using System.ComponentModel.DataAnnotations; 

namespace StudentAutomation.Frontend.Models
{

    public class LoginModel
    {
        [Required(ErrorMessage = "Email gerekli")]
        [EmailAddress(ErrorMessage = "Geçerli bir email girin")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gerekli")]
        public string PasswordHash  { get; set; }
    }
}