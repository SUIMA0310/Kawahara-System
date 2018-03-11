using System;
using System.Windows.Forms;

namespace DesktopApp.Services
{
    public class OverlayWindowService : WindowServiceBase<IWindowFactory, IOverlayWindowController>, IOverlayWindowService
    {
        public Screen UseScreen
        {
            get => this.WindowController?.UseScreen;
            set {
                if ( this.WindowController == null ) {
                    return;
                }
                this.WindowController.UseScreen = value;
                OnUseScreenChanged( value );
            }
        }

        public event EventHandler UseScreenChanged;

        protected void OnUseScreenChanged( Screen screen )
        {
            this.UseScreenChanged?.Invoke( this, EventArgs.Empty );
        }
    }
}