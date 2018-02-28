using System;

namespace DesktopApp.Services
{
    public interface IWindowController
    {
        
        /// <summary>
        /// Windowの可視状態
        /// </summary>
        bool IsHidden { get; set; }

        /// <summary>
        /// Windowを閉じる
        /// </summary>
        void Close();

        /// <summary>
        /// Windowの初期化が完了
        /// </summary>
        event Action WindowInitialized;

        /// <summary>
        /// WindowのCloseが完了 もしくは、 それ以上の状態追跡ができない
        /// </summary>
        event Action WindowClosed;

        /// <summary>
        /// Windowの可視状態が変化
        /// </summary>
        event Action<bool> HiddenChanged;

    }
}
