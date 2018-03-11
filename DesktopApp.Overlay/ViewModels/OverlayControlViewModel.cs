using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Forms;

using DesktopApp.Services;

using Prism.Interactivity.InteractionRequest;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace DesktopApp.ViewModels
{
    public class OverlayControlViewModel : ViewModelBase, IWindowFactory
    {
        private IOverlayWindowService OverlayWindowService;

        public InteractionRequest<INotification> PopupOverlayWindowRequest { get; }

        public ReadOnlyReactiveProperty<string> ShowOverlayWindowState { get; }
        public ReadOnlyReactiveProperty<bool> CanOperationToOverlayWindow { get; }
        public ReactiveProperty<bool> ShowOverlayWindowRequest { get; }

        public ReactiveCollection<Screen> ScreenCollection { get; }
        public ReactiveProperty<Screen> UseScreen { get; }

        public ReactiveCommand ScreenUpdateCommand { get; }

        public OverlayControlViewModel( IOverlayWindowService windowService )
        {
            this.Title.Value = "Overlay";

            this.OverlayWindowService = windowService;
            this.OverlayWindowService.SetWindowFactory( this );

            this.PopupOverlayWindowRequest = new InteractionRequest<INotification>();

            var ObservableWindowState = Observable.FromEvent<eWindowStateTypes>(
                handler => this.OverlayWindowService.WindowStateChanged += handler,
                handler => this.OverlayWindowService.WindowStateChanged -= handler
            );

            this.ShowOverlayWindowState = ObservableWindowState
                .Select( x => eWindowStateTypes.Shown == x )
                .Select( x => x ? "表示 ON" : "表示 OFF" )
                .ToReadOnlyReactiveProperty( "表示 OFF" )
                .AddTo( this.Disposable );

            this.CanOperationToOverlayWindow = ObservableWindowState
                .Select( x => eWindowStateTypes.Initializeing != x )
                .ToReadOnlyReactiveProperty( true )
                .AddTo( this.Disposable );

            this.ShowOverlayWindowRequest = new ReactiveProperty<bool>();

            this.ShowOverlayWindowRequest
                .Where( x => x )
                .Subscribe( _ => this.OverlayWindowService.Show() )
                .AddTo( this.Disposable );

            this.ShowOverlayWindowRequest
                .Where( x => !x )
                .Subscribe( _ => this.OverlayWindowService.Hide() )
                .AddTo( this.Disposable );

            this.ScreenCollection = new ReactiveCollection<Screen>();
            this.ScreenCollection.AddRangeOnScheduler( Screen.AllScreens );

            this.ScreenUpdateCommand = new ReactiveCommand();
            this.ScreenUpdateCommand
                .Subscribe( _ =>
                 {
                     this.ScreenCollection.ClearOnScheduler();
                     this.ScreenCollection.AddRangeOnScheduler( Screen.AllScreens );
                 } )
                .AddTo( this.Disposable );

            this.UseScreen = Observable.FromEventPattern
                (
                    handler => this.OverlayWindowService.UseScreenChanged += handler,
                    handler => this.OverlayWindowService.UseScreenChanged -= handler
                )
                .Select( _ => this.OverlayWindowService.UseScreen )
                .ToReactiveProperty( Screen.PrimaryScreen )
                .AddTo( this.Disposable );
            this.UseScreen
                .Where( x => x != null )
                .Subscribe( x => this.OverlayWindowService.UseScreen = x )
                .AddTo( this.Disposable );
        }

        public override void Dispose()
        {
            base.Dispose();
            this.OverlayWindowService.SetWindowFactory( null );
        }

        void IWindowFactory.Create()
        {
            this.PopupOverlayWindowRequest.Raise( new Prism.Interactivity.InteractionRequest.Notification { Title = "OverlayWindow" } );
        }
    }
}