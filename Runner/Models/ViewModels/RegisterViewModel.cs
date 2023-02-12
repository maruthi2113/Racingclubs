using System.ComponentModel.DataAnnotations;

namespace Runner.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name ="Email Address")]
        [Required]
        public string Email { get; set; }
       
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password Doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}
