using System;

namespace DesktopApp.Services
{
    public interface IOverlayWindowService : IWindowService
    {
        event EventHandler UseScreenChanged;

        System.Windows.Forms.Screen UseScreen { get; set; }
    }
}