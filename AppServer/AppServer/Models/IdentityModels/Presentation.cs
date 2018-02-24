﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppServer.Models
{
    /// <summary>
    /// プレゼンテーション
    /// </summary>
    public class Presentation
    {

        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Guid Id { get; set; }

        /// <summary>
        /// Presentationの管理名（重複を許容）
        /// </summary>
        [Required]
        [MaxLength( 256 )]
        public string Name { get; set; }

        /// <summary>
        /// 利用可能なリアクション
        /// </summary>
        [Required]
        public eReactionType HasReactionType { get; set; } = eReactionType.Good;

        /// <summary>
        /// 合計リアクション回数
        /// </summary>
        public ulong ReactionCount { get; set; } = 0;

        /// <summary>
        /// 所有者
        /// </summary>
        [Required]
        public virtual ApplicationUser Owner { get; set; }

    }
}