using System.ComponentModel.DataAnnotations;

namespace MyApp.Presentation.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Please enter your email.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
    }
}
