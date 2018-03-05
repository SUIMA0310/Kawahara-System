using System;

namespace DesktopApp.Services
{
    public interface IHubProxyService
    {
        string ServerURL { get; set; }

        event Action<string> ServerURLChanged;

        void Open();
    }
}