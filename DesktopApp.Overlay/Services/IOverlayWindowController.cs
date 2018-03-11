namespace DesktopApp.Services
{
    public interface IOverlayWindowController : IWindowController
    {
        System.Windows.Forms.Screen UseScreen { get; set; }
    }
}