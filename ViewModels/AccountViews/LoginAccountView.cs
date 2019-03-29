using System.ComponentModel.DataAnnotations;

namespace BJ.ViewModels.AccountViews
{
    public class LoginAccountView
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
