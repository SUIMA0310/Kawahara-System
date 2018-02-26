using Microsoft.AspNet.SignalR.Client;

namespace DesktopApp.Services
{
    public interface IConnectionService
    {
        bool HasConnection { get; }

        IHubProxy CreateHubProxy(string hubName);
        void Open();
        void Close();
    }
}