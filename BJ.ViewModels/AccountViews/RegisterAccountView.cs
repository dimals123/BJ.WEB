using System.ComponentModel.DataAnnotations;

namespace BJ.ViewModels.AccountViews
{
    public class RegisterAccountView
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }


    }
}
