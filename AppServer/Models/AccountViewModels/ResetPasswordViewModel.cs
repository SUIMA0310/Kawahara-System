using System.ComponentModel.DataAnnotations;

namespace AppServer.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display( Name = "電子メール" )]
        public string Email { get; set; }

        [Required]
        [StringLength( 100, ErrorMessage = "{0} の長さは {2} 文字以上である必要があります。", MinimumLength = 6 )]
        [DataType( DataType.Password )]
        [Display( Name = "パスワード" )]
        public string Password { get; set; }

        [DataType( DataType.Password )]
        [Display( Name = "パスワードの確認入力" )]
        [Compare( "Password", ErrorMessage = "パスワードと確認のパスワードが一致しません。" )]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}