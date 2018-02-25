using System.ComponentModel.DataAnnotations;

namespace AppServer.Models
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "電話番号")]
        public string Number { get; set; }
    }
}