using System;
using System.Windows;
using DesktopApp.Services;

namespace DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for OverlayWindow.xaml
    /// </summary>
    public partial class OverlayWindow : Overlay.Core.OverlayWindow, IWindowController
    {
        public OverlayWindow(IWindowService windowService )
        {

            windowService.SetWindowController( this );

            InitializeComponent();
        }

        bool IWindowController.IsHidden
        {
            get => this.Opacity == 0.0;
            set {

                if (value) {

                    this.Opacity = 0.0;

                } else {

                    this.Opacity = 1.0;

                }

                this.HiddenChanged?.Invoke(value);

            }
        }

        public event Action WindowInitialized;
        public event Action WindowClosed;
        public event Action<bool> HiddenChanged;

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.WindowInitialized?.Invoke();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this.WindowClosed?.Invoke();
        }

    }
}
