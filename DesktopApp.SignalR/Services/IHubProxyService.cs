using System;

namespace DesktopApp.Services
{
    public interface IHubProxyService
    {
        string ServerURL { get; set; }

        event Action<string> ServerURLChanged;

        event Action Connected;

        void Open();
    }
}