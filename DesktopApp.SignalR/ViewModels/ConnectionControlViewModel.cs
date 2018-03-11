using System;
using System.Linq;
using System.Reactive.Linq;

using DesktopApp.Helpers;
using DesktopApp.Notifications;
using DesktopApp.Services;

using Prism.Interactivity.InteractionRequest;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace DesktopApp.ViewModels
{
    public class ConnectionControlViewModel : ViewModelBase
    {
        private readonly IReactionHubProxy ReactionHub;
        private readonly IConnectionService ConnectionService;

        public ReactiveProperty<string> ServerURL { get; }
        public ReactiveProperty<string> PresentationID { get; }
        public ReadOnlyReactiveProperty<string> ConnectionState { get; }

        public ReactiveCommand SettingChangeCommand { get; }

        public InteractionRequest<InputSettingNotification> InputSettingRequest { get; }
        public InteractionRequest<Notification> ExceptionNotificationRequest { get; }

        public ConnectionControlViewModel( IReactionHubProxy reactionHubProxy, IConnectionService connectionService )
        {
            this.ReactionHub = reactionHubProxy;
            this.ConnectionService = connectionService;

            this.ServerURL = Observable.FromEvent<string>(
                handler => this.ReactionHub.ServerURLChanged += handler,
                handler => this.ReactionHub.ServerURLChanged -= handler )
                .Select( x => x ?? "N/A" )
                .ToReactiveProperty( Properties.Settings.Default.LastServerURL.NotEmptyOrDefault( "N/A" ) )
                .AddTo( this.Disposable );
            this.ServerURL
                .Where( x => x != "N/A" )
                .Select( x => string.IsNullOrWhiteSpace( x ) ? null : x )
                .Subscribe( x => this.ReactionHub.ServerURL = x )
                .AddTo( this.Disposable );

            this.PresentationID = Observable.FromEvent<Action<string, string>, string>(
                handler => ( newValue, oldValue ) => handler( newValue ),
                handler => this.ReactionHub.PresentationIDChanged += handler,
                handler => this.ReactionHub.PresentationIDChanged -= handler )
                .Select( x => x ?? "N/A" )
                .ToReactiveProperty( Properties.Settings.Default.LastPresentationID.NotEmptyOrDefault( "N/A" ) )
                .AddTo( this.Disposable );
            this.PresentationID
                .Where( x => x != "N/A" )
                .Select( x => string.IsNullOrWhiteSpace( x ) ? null : x )
                .Subscribe( x => this.ReactionHub.PresentationID = x )
                .AddTo( this.Disposable );

            this.ConnectionState = Observable.FromEvent<bool>(
                handler => this.ConnectionService.HasConnectionChanged += handler,
                handler => this.ConnectionService.HasConnectionChanged -= handler )
                .Select( x => x ? "接続完了" : "切断" )
                .ToReadOnlyReactiveProperty( this.ConnectionService.HasConnection ? "接続完了" : "切断" )
                .AddTo( this.Disposable );

            this.SettingChangeCommand = new ReactiveCommand();
            this.SettingChangeCommand
                .Subscribe( _ =>
                 {
                     var inputSetting = new InputSettingNotification { Title = "通信設定 ・ 変更" };
                     this.InputSettingRequest.Raise( inputSetting );

                     if ( inputSetting.Confirmed ) {
                         this.ReactionHub.PresentationID = inputSetting.InputPresentationID;

                         try {
                             this.ReactionHub.ServerURL = inputSetting.InputServerURL;
                         } catch ( InvalidOperationException ex ) {
                             inputSetting.InputServerURL = this.ReactionHub.ServerURL;

                             this.ExceptionNotificationRequest.Raise(
                                 new Notification {
                                     Title = "ERROR",
                                     Content = ex.Message
                                 } );
                         }
                     }
                 } )
                .AddTo( this.Disposable );

            this.InputSettingRequest = new InteractionRequest<InputSettingNotification>();
            this.ExceptionNotificationRequest = new InteractionRequest<Notification>();

            this.Title.Value = "Connection";
        }
    }
}