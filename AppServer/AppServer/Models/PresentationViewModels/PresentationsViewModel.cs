using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppServer.Models.PresentationViewModels
{
    public class PresentationsViewModel
    {

        /// <summary>
        /// プレゼンテーションの名前
        /// </summary>
        [Required]
        [MaxLength( 256 )]
        public string Name { get; set; }

        /// <summary>
        /// 利用するリアクションの種類
        /// </summary>
        public eReactionType HasReactionType { get; set; }

        /// <summary>
        /// Goodを利用するか
        /// </summary>
        public bool HasGood {
            get {
                return HasReactionType.HasFlag( eReactionType.Good );
            }
            set {
                if (value) {
                    HasReactionType |= eReactionType.Good;
                } else {
                    HasReactionType &= ~eReactionType.Good;
                }
            }
        }

        /// <summary>
        /// Niceを利用するか
        /// </summary>
        public bool HasNice {
            get {
                return HasReactionType.HasFlag( eReactionType.Nice );
            }
            set {
                if (value) {
                    HasReactionType |= eReactionType.Nice;
                } else {
                    HasReactionType &= ~eReactionType.Nice;
                }
            }
        }

        /// <summary>
        /// Funを利用するか
        /// </summary>
        public bool HasFun {
            get {
                return HasReactionType.HasFlag( eReactionType.Fun );
            }
            set {
                if (value) {
                    HasReactionType |= eReactionType.Fun;
                } else {
                    HasReactionType &= ~eReactionType.Fun;
                }
            }
        }

    }
}