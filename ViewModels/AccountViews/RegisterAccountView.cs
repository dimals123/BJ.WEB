using System.ComponentModel.DataAnnotations;

namespace ViewModels.AccountViews
{
    public class RegisterAccountView
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}
