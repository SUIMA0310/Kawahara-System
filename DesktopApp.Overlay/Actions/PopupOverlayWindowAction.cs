using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Interactivity.InteractionRequest;

namespace DesktopApp.Actions
{
    public class PopupOverlayWindowAction : Prism.Interactivity.PopupWindowAction
    {
        protected override Window CreateWindow()
        {
            return new Overlay.Core.OverlayWindow();
        }

        protected override Window GetWindow(INotification notification)
        {
            return base.GetWindow(notification);
        }
    }
}
