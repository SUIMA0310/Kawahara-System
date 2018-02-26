using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using DesktopApp.Services;

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
