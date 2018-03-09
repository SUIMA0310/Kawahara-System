using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace DesktopApp.ViewModels
{
    public class InputSettingViewModel : ViewModelBase, IInteractionRequestAware
    {

        public INotification Notification
        {
            get => this._Notification;
            set {
                this._Notification = value as Notifications.IInputSettingNotification;
                this.Initialize();
            }
        }
        public Action FinishInteraction { get; set; }


        public ReactiveProperty<string> ServerURL { get; }
        public ReactiveProperty<string> PresentetionID { get; }


        public ReactiveCommand OK { get; }
        public ReactiveCommand Cancel { get; }


        public InputSettingViewModel()
        {
            this.ServerURL = new ReactiveProperty<string>();
            this.PresentetionID = new ReactiveProperty<string>();

            this.OK = new ReactiveCommand();
            this.OK
                .Subscribe(_ =>
                {
                    this._Notification.InputServerURL = this.ServerURL.Value;
                    this._Notification.InputPresentationID = this.PresentetionID.Value;
                    this._Notification.Confirmed = true;
                    this.OnFinishInteraction();
                })
                .AddTo(this.Disposable);

            this.Cancel = new ReactiveCommand();
            this.Cancel
                .Subscribe(_ =>
                {
                    this._Notification.Confirmed = false;
                    this.OnFinishInteraction();
                })
                .AddTo(this.Disposable);

        }


        protected virtual void Initialize()
        {
            this.ServerURL.Value = this._Notification.InputServerURL;
            this.PresentetionID.Value = this._Notification.InputPresentationID;

            if ( string.IsNullOrWhiteSpace(this.ServerURL.Value) ) {

                this.ServerURL.Value = Properties.Resources.DefaultServerURL;

            }
        }

        protected virtual void OnFinishInteraction()
        {
            this.FinishInteraction?.Invoke();
        }

        private Notifications.IInputSettingNotification _Notification;
    }
}
