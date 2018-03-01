using System;
using System.Windows;
using DesktopApp.Services;

namespace DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for OverlayWindow.xaml
    /// </summary>
    public partial class OverlayWindow : Overlay.Core.OverlayWindow, IOverlayWindowController
    {
        public OverlayWindow(IOverlayWindowService windowService )
        {
            windowService.SetWindowController( this );

            InitializeComponent();
        }

    }
}
