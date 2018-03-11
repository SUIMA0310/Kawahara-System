using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;

namespace DesktopApp.Overlay.Core
{
    public class OverlayWindow : Window
    {
        #region DependencyProperties

        #region AltF4Cancel

        public bool AltF4Cancel
        {
            get { return (bool)GetValue( AltF4CancelProperty ); }
            set { SetValue( AltF4CancelProperty, value ); }
        }

        // Using a DependencyProperty as the backing store for AltF4Cancel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AltF4CancelProperty =
            DependencyProperty.Register(
                "AltF4Cancel",
                typeof(bool),
                typeof(OverlayWindow),
                new PropertyMetadata(true));

        #endregion AltF4Cancel

        #region ShowSystemMenu

        public bool ShowSystemMenu
        {
            get { return (bool)GetValue( ShowSystemMenuProperty ); }
            set { SetValue( ShowSystemMenuProperty, value ); }
        }

        // Using a DependencyProperty as the backing store for ShowSystemMenu.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowSystemMenuProperty =
            DependencyProperty.Register(
                "ShowSystemMenu",
                typeof(bool),
                typeof(OverlayWindow),
                new PropertyMetadata(
                    false,
                    ShowSystemMenuPropertyChanged));

        private static void ShowSystemMenuPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            if ( d is OverlayWindow window && e.NewValue is bool value ) {
                window.SetShowSystemMenu( value );
            }
        }

        #endregion ShowSystemMenu

        #region ClickThrough

        public bool ClickThrough
        {
            get { return (bool)GetValue( ClickThroughProperty ); }
            set { SetValue( ClickThroughProperty, value ); }
        }

        // Using a DependencyProperty as the backing store for ClickThrough.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClickThroughProperty =
            DependencyProperty.Register(
                "ClickThrough",
                typeof(bool),
                typeof(OverlayWindow),
                new PropertyMetadata(
                    true,
                    ClickThroughPropertyChanged));

        private static void ClickThroughPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            if ( d is OverlayWindow window && e.NewValue is bool value ) {
                window.SetClickThrough( value );
            }
        }

        #endregion ClickThrough

        #region UseScreen

        public Screen UseScreen
        {
            get { return (Screen)GetValue( UseScreenProperty ); }
            set { SetValue( UseScreenProperty, value ); }
        }

        // Using a DependencyProperty as the backing store for UseScreen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseScreenProperty =
            DependencyProperty.Register(
                "UseScreen",
                typeof(Screen),
                typeof(OverlayWindow),
                new PropertyMetadata(
                    Screen.PrimaryScreen,
                    UseScreenPropertyChanged));

        private static void UseScreenPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            if ( d is OverlayWindow window && e.NewValue is Screen screen ) {
                window.SetUseScreen( screen );
            }
        }

        #endregion UseScreen

        #endregion DependencyProperties

        #region const values

        private const int GWL_STYLE = (-16); // ウィンドウスタイル
        private const int GWL_EXSTYLE = (-20); // 拡張ウィンドウスタイル

        private const int WS_SYSMENU = 0x00080000; // システムメニュを表示する
        private const int WS_EX_TRANSPARENT = 0x00000020; // 透過ウィンドウスタイル

        private const int WM_SYSKEYDOWN = 0x0104; // Alt + 任意のキー の入力
        private const int WM_DPICHANGED = 0x02E0; // DPI変更通知

        private const int VK_F4 = 0x73;

        private const int SWP_NOACTIVATE = 0x0010;

        private const int HWND_TOPMOST = -1;

        #endregion const values

        #region Win32Apis

        [DllImport( "user32" )]
        private static extern int GetWindowLong( IntPtr hWnd, int nIndex );

        [DllImport( "user32" )]
        private static extern int SetWindowLong( IntPtr hWnd, int nIndex, int dwLong );

        [DllImport( "user32" )]
        private static extern int SetWindowPos( IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int uFlags );

        #endregion Win32Apis

        public OverlayWindow()
        {
            // 透過背景
            this.WindowStyle = WindowStyle.None;
            this.AllowsTransparency = true;
            this.Background = new SolidColorBrush( Colors.Transparent );

            // 最前面表示
            this.Topmost = true;

            //タスクバーに表示しない
            this.ShowInTaskbar = false;
        }

        protected override void OnSourceInitialized( EventArgs e )
        {
            //システムメニュを非表示
            this.SetShowSystemMenu( this.ShowSystemMenu );

            //クリックをスルー
            this.SetClickThrough( this.ClickThrough );

            // 全画面表示
            this.SetUseScreen( this.UseScreen );

            //Alt + F4 を無効化
            var handle = new WindowInteropHelper(this).Handle;
            var hwndSource = HwndSource.FromHwnd(handle);
            hwndSource.AddHook( WndProc );

            base.OnSourceInitialized( e );
        }

        protected virtual IntPtr WndProc( IntPtr hwnd, int msg, IntPtr wParam, IntPtr IParam, ref bool handled )
        {
            //Alt + F4 が入力されたら
            if ( msg == WM_SYSKEYDOWN && wParam.ToInt32() == VK_F4 ) {
                if ( this.AltF4Cancel ) {
                    //処理済みにセットする
                    //(Windowは閉じられなくなる)
                    handled = true;
                }
            }

            return IntPtr.Zero;
        }

        /// <summary>
        /// システムメニュの表示を切り替える
        /// </summary>
        /// <param name="value"><see langword="true"/> = 表示, <see langword="false"/> = 非表示</param>
        private void SetShowSystemMenu( bool value )
        {
            var handle = new WindowInteropHelper(this).Handle;
            if ( handle == IntPtr.Zero ) { return; }

            int windowStyle = GetWindowLong(handle, GWL_STYLE);

            if ( value ) {
                windowStyle |= WS_SYSMENU; //フラグの追加
            } else {
                windowStyle &= ~WS_SYSMENU; //フラグを消す
            }

            SetWindowLong( handle, GWL_STYLE, windowStyle );
        }

        /// <summary>
        /// クリックスルーの設定
        /// </summary>
        /// <param name="value"><see langword="true"/> = クリックをスルー, <see langword="false"/>=クリックを捉える</param>
        private void SetClickThrough( bool value )
        {
            var handle = new WindowInteropHelper(this).Handle;
            if ( handle == IntPtr.Zero ) { return; }

            int extendStyle = GetWindowLong(handle, GWL_EXSTYLE);

            if ( value ) {
                extendStyle |= WS_EX_TRANSPARENT; //フラグの追加
            } else {
                extendStyle &= ~WS_EX_TRANSPARENT; //フラグを消す
            }

            SetWindowLong( handle, GWL_EXSTYLE, extendStyle );
        }

        /// <summary>
        /// Windowを指定Screen上に移動する
        /// </summary>
        /// <param name="screen">表示するScreen</param>
        private void SetUseScreen( Screen screen )
        {
            if ( screen == null ) { return; }

            var handle = new WindowInteropHelper(this).Handle;
            if ( handle == IntPtr.Zero ) { return; }

            SetWindowPos(
                handle,
                HWND_TOPMOST,
                screen.Bounds.Left,
                screen.Bounds.Top,
                screen.Bounds.Width,
                screen.Bounds.Height,
                SWP_NOACTIVATE );
            //this.Top = screen.Bounds.Top;
            //this.Left = screen.Bounds.Left;
            //this.Height = screen.Bounds.Height;
            //this.Width = screen.Bounds.Width;
        }

        public Point GetDpiScaleFactor()
        {
            Visual visual = this;
            var source = PresentationSource.FromVisual(visual);
            if ( source != null && source.CompositionTarget != null ) {
                return new Point(
                    source.CompositionTarget.TransformToDevice.M11,
                    source.CompositionTarget.TransformToDevice.M22 );
            }

            return new Point( 1.0, 1.0 );
        }
    }
}