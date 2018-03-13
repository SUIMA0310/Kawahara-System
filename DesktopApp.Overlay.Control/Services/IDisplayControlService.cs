using DesktopApp.Overlay.Draw.Models;

using Reactive.Bindings;

namespace DesktopApp.Services
{
    public interface IDisplayControlService
    {
        ReactiveProperty<float>           DisplayTime  { get; }
        ReactiveProperty<float>           MaxOpacity   { get; }
        ReactiveProperty<float>           Scale        { get; }
        ReactiveProperty<IParameterCurve> MoveMethod   { get; }
        ReactiveProperty<IParameterCurve> OpacityCurve { get; }

        void Load();

        void Save();
    }
}