using System.ComponentModel.DataAnnotations;

namespace AppServer.Models
{
    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display( Name = "コード" )]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display( Name = "電話番号" )]
        public string PhoneNumber { get; set; }
    }
}