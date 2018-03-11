using System;

namespace DesktopApp.Models
{
    /// <summary>
    /// リアクションの種類
    /// </summary>
    [Flags]
    public enum eReactionType
    {
        /// <summary>
        /// いいね！
        /// </summary>
        Good = 0x01,

        /// <summary>
        /// ナイス！
        /// </summary>
        Nice = 0x02,

        /// <summary>
        /// おもしろいね！
        /// </summary>
        Fun = 0x04
    }
}