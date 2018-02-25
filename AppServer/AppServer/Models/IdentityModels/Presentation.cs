using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppServer.Models
{
    /// <summary>
    /// プレゼンテーション
    /// </summary>
    public class Presentation
    {

        public Presentation()
        {

            this.Id = Guid.NewGuid().ToString();

        }

        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Presentationの管理名（重複を許容）
        /// </summary>
        [Required]
        [MaxLength( 256 )]
        [Display( Name = "名前" )]
        public string Name { get; set; }

        /// <summary>
        /// 利用可能なリアクション
        /// </summary>
        [Required]
        [Display( Name = "リアクション" )]
        public eReactionType HasReactionType { get; set; } = eReactionType.Good;

        /// <summary>
        /// 合計リアクション回数
        /// </summary>
        [Required]
        [Display( Name = "リアクション回数" )]
        public long ReactionCount { get; set; }

        /// <summary>
        /// 所有者
        /// </summary>
        [Required]
        public virtual ApplicationUser Owner { get; set; }

    }
}