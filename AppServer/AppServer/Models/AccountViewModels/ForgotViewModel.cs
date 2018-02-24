using System.ComponentModel.DataAnnotations;

namespace AppServer.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "電子メール")]
        public string Email { get; set; }
    }
}
