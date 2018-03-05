using System.ComponentModel.DataAnnotations;

namespace AppServer.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "電子メール")]
        public string Email { get; set; }
    }
}
