using System;

using Microsoft.AspNet.SignalR.Client;

namespace DesktopApp.Services
{
    public interface IConnectionService
    {
        bool HasConnection { get; }
        string ServerURL { get; set; }

        event Action<bool> HasConnectionChanged;

        event Action<string> ServerURLChanged;

        event Action Connected;

        IHubProxy CreateHubProxy( string hubName );

        void Open();

        void Close();
    }
}