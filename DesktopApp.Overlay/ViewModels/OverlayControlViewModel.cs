using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Linq;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using DesktopApp.Services;
using Prism.Interactivity.InteractionRequest;

namespace DesktopApp.ViewModels
{
    public class OverlayControlViewModel : ViewModelBase, IWindowFactory
    {

        private IWindowService WindowService;

        public InteractionRequest<INotification> PopupOverlayWindowRequest { get; } 

        public ReadOnlyReactiveProperty<string> ShowOverlayWindowState { get; }
        public ReadOnlyReactiveProperty<bool> CanOperationToOverlayWindow { get; }
        public ReactiveProperty<bool> ShowOverlayWindowRequest { get; }


        public OverlayControlViewModel(IWindowService windowService)
        {
            this.Title.Value = "Overlay";


            this.WindowService = windowService;
            this.WindowService.SetWindowFactory(this);


            this.PopupOverlayWindowRequest = new InteractionRequest<INotification>();


            var ObservableWindowState = Observable.FromEvent<eWindowStateTypes>(
                handler => this.WindowService.WindowStateChanged += handler,
                handler => this.WindowService.WindowStateChanged -= handler
            );


            this.ShowOverlayWindowState = ObservableWindowState
                .Select(x => eWindowStateTypes.Shown == x)
                .Select(x => x ? "表示 ON" : "表示 OFF")
                .ToReadOnlyReactiveProperty("表示 OFF")
                .AddTo(this.Disposable);

            this.CanOperationToOverlayWindow = ObservableWindowState
                .Select(x => eWindowStateTypes.Initializeing != x)
                .ToReadOnlyReactiveProperty( true )
                .AddTo(this.Disposable);


            this.ShowOverlayWindowRequest = new ReactiveProperty<bool>();

            this.ShowOverlayWindowRequest
                .Where(x => x)
                .Subscribe(_ => this.WindowService.Show())
                .AddTo(this.Disposable);

            this.ShowOverlayWindowRequest
                .Where(x => !x)
                .Subscribe(_ => this.WindowService.Hide())
                .AddTo(this.Disposable);

        }

        public override void Dispose()
        {
            base.Dispose();
            this.WindowService.SetWindowFactory(null);
        }

        void IWindowFactory.Create()
        {
            this.PopupOverlayWindowRequest.Raise(new Prism.Interactivity.InteractionRequest.Notification { Title = "OverlayWindow" });
        }
    }
}
