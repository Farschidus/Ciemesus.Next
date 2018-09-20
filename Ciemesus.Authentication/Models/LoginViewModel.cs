using System.ComponentModel.DataAnnotations;

namespace Ciemesus.Authentication.Models
{
    public class LoginViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberLogin { get; set; }

        public string ReturnUrl { get; set; }
    }
}
