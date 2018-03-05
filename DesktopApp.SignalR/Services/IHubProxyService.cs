namespace DesktopApp.Services
{
    public interface IHubProxyService
    {
        string ServerURL { get; set; }

        void Open();
    }
}