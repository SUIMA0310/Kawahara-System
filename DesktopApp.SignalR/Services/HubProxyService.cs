﻿using System;

using Microsoft.AspNet.SignalR.Client;

using Prism.Logging;

namespace DesktopApp.Services
{
    public abstract class HubProxyService : IHubProxyService
    {
        protected ILoggerFacade Logger { get; }
        protected IConnectionService Connection { get; }
        protected IHubProxy Proxy { get; private set; }

        public string ServerURL
        {
            get => this.Connection.ServerURL;
            set => this.Connection.ServerURL = value;
        }

        public HubProxyService( ILoggerFacade logger, IConnectionService connection )
        {
            this.Logger = logger;
            this.Connection = connection ?? throw new ArgumentNullException( nameof( connection ) );
            this.Connection.ServerURLChanged += ( e ) => { this.OnServerURLChanged( e ); };
            this.Connection.Connected += () => { this.OnConnected(); };
        }

        public virtual void Open()
        {
            this.Proxy = this.Connection.CreateHubProxy( this.HubName );
            this.PreInitializeProxy();
            this.Connection.Open();
            this.PostInitializeProxy();
        }

        protected abstract string HubName { get; }

        protected virtual void PreInitializeProxy()
        {
        }

        protected virtual void PostInitializeProxy()
        {
        }

        public event Action<string> ServerURLChanged;

        public event Action Connected;

        protected virtual void OnServerURLChanged( string args )
        {
            this.ServerURLChanged?.Invoke( args );
        }

        protected virtual void OnConnected()
        {
            this.Connected?.Invoke();
        }
    }
}