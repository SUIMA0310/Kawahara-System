﻿using DesktopApp.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Linq;
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

        public ConnectionControlViewModel(IReactionHubProxy reactionHubProxy, IConnectionService connectionService)
        {
            this.ReactionHub = reactionHubProxy;
            this.ConnectionService = connectionService;

            this.ServerURL = Observable.FromEvent<string>(
                handler => this.ReactionHub.ServerURLChanged += handler,
                handler => this.ReactionHub.ServerURLChanged -= handler)
                .Select(x => x ?? "N/A")
                .ToReactiveProperty(this.ReactionHub.ServerURL ?? "N/A")
                .AddTo(this.Disposable);
            this.ServerURL
                .Where( x => x != "N/A" )
                .Subscribe(x => this.ReactionHub.ServerURL = x)
                .AddTo(this.Disposable);

            this.PresentationID = Observable.FromEvent<string>(
                handler => this.ReactionHub.PresentationIDChanged += handler,
                handler => this.ReactionHub.PresentationIDChanged -= handler)
                .Select(x => x ?? "N/A")
                .ToReactiveProperty(this.ReactionHub.PresentationID ?? "N/A")
                .AddTo(this.Disposable);
            this.PresentationID
                .Where(x => x != "N/A")
                .Subscribe(x => this.ReactionHub.PresentationID = x)
                .AddTo(this.Disposable);

            this.ConnectionState = Observable.FromEvent<bool>(
                handler => this.ConnectionService.HasConnectionChanged += handler,
                handler => this.ConnectionService.HasConnectionChanged -= handler)
                .Select(x => x ? "接続中" : "切断")
                .ToReadOnlyReactiveProperty(this.ConnectionService.HasConnection ? "接続中" : "切断")
                .AddTo(this.Disposable);

            this.Title.Value = "Connection";
        }
    }
}
