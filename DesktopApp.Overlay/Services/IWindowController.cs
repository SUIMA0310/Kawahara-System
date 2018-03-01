using System;

namespace DesktopApp.Services
{
    public interface IWindowController
    {
        
        /// <summary>
        /// Windowの可視状態
        /// </summary>
        double Opacity { get; set; }

        /// <summary>
        /// Windowを閉じる
        /// </summary>
        void Close();

        /// <summary>
        /// Windowの初期化が完了
        /// </summary>
        event EventHandler Initialized;

        /// <summary>
        /// WindowのCloseが完了 もしくは、 それ以上の状態追跡ができない
        /// </summary>
        event EventHandler Closed;

    }
}
