using System.ComponentModel.DataAnnotations;

namespace AppServer.Models.PresentationViewModels
{
    public class EditViewModel
    {

        /// <summary>
        /// プレゼンテーションのId
        /// </summary>
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// プレゼンテーションの名前
        /// </summary>
        [Required]
        [MaxLength( 256 )]
        [Display( Name = "名前" )]
        public string Name { get; set; }

        /// <summary>
        /// 利用するリアクションの種類
        /// </summary>
        [Display( Name = "利用するリアクションタイプ" )]
        public eReactionType HasReactionType { get; set; }


    }
}