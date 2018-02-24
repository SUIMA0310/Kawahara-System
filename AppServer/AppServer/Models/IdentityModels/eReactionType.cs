using System;

namespace AppServer.Models
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
        Good,

        /// <summary>
        /// ナイス！
        /// </summary>
        Nice,

        /// <summary>
        /// おもしろいね！
        /// </summary>
        Fun

    }
}