using System.ComponentModel.DataAnnotations;

namespace ViewModels.AccountViews
{
    public class RegisterAccountView
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }
    }
}
