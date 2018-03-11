using DesktopApp.Services;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace DesktopApp.ViewModels
{
    public class StatusBarControlViewModel : ViewModelBase
    {
        public ReactiveProperty<string> StatusMessage { get; }

        public StatusBarControlViewModel( IStatusService statusService )
        {
            this.Title.Value = "StatusBar";
            this.StatusMessage = statusService
                .Status
                .ToReactiveProperty()
                .AddTo( this.Disposable );
        }
    }
}