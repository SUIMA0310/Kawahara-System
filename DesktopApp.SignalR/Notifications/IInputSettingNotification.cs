using Prism.Interactivity.InteractionRequest;

namespace DesktopApp.Notifications
{
    public interface IInputSettingNotification : IConfirmation
    {
        string InputPresentationID { get; set; }
        string InputServerURL { get; set; }
    }
}