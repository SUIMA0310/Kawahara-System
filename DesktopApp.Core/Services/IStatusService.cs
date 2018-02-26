using Reactive.Bindings;

namespace DesktopApp.Services
{
    public interface IStatusService
    {
        ReactiveProperty<string> Status { get; }

        void SetInfomation(string msg);
        void SetStatus(eStatusMessages statusMessages);
        void SetStatus(string msg);
    }
}