using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Runner.Models.ViewModels
{
    public class LoginViewModel
    {

        [Display(Name="Email Address")]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }    
    }
}
