using Microsoft.AspNet.SignalR.Client;

namespace DesktopApp.Services
{
    public interface IConnectionService
    {
        bool HasConnection { get; }
        string ServerURL { get; set; }

        IHubProxy CreateHubProxy(string hubName);
        void Open();
        void Close();
    }
}