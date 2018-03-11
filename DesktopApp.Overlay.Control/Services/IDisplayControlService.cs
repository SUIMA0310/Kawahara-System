using Reactive.Bindings;

namespace DesktopApp.Services
{
    public interface IDisplayControlService
    {
        ReactiveProperty<float> DisplayTime { get; }
        ReactiveProperty<float> MaxOpacity { get; }
        ReactiveProperty<float> Scale { get; }

        void Load();

        void Save();
    }
}