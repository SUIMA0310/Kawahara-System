using System;

namespace DesktopApp.Services
{
    public interface IWindowService
    {
        eWindowStateTypes WindowState { get; }

        event Action<eWindowStateTypes> WindowStateChanged;

        void Hide();
        bool SetWindowController(IWindowController windowController, bool throwException = true);
        bool SetWindowFactory(IWindowFactory windowFactory, bool throwException = true);
        void Show();
    }
}