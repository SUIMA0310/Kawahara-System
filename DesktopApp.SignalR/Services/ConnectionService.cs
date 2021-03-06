﻿using System;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR.Client;

using Prism.Logging;

namespace DesktopApp.Services
{
    /// <summary>
    /// SignalRとの通信を行う
    /// </summary>
    public class ConnectionService : IDisposable, IConnectionService
    {
        /// <summary>
        /// Openが実行されたか
        /// </summary>
        private bool _IsOpened = false;

        private string _ServerURL = null;

        private ILoggerFacade Logger { get; }
        private HubConnection Connection;
        private Task Connecting;

        /// <summary>
        /// 現在接続を完了しているか
        /// </summary>
        public bool HasConnection => this.Connection?.State == ConnectionState.Connected;

        /// <summary>
        /// Serverの接続先
        /// </summary>
        public string ServerURL
        {
            get => this._ServerURL;
            set {
                if ( this._ServerURL == value ) { return; }
                if ( this._IsOpened ) { throw new InvalidOperationException( "接続開始後は変更できません" ); }
                this._ServerURL = value;
                this.OnServerURLChanged( value );
            }
        }

        public ConnectionService( ILoggerFacade logger )
        {
            this.Logger = logger;
        }

        public IHubProxy CreateHubProxy( string hubName )
        {
            if ( this.Connection == null ) { this.Connection = this.CreateHubConnection(); }
            return this.Connection.CreateHubProxy( hubName );
        }

        public void Open()
        {
            //2度目以降の実行を無視
            if ( this._IsOpened ) { return; }
            this._IsOpened = true;

            if ( this.Connection == null ) { this.Connection = this.CreateHubConnection(); }
            this.Connection.StateChanged += ( e ) =>
            {
                this.OnHasConnectionChanged( this.HasConnection );
            };
            this.Connection.Error += this.Connection_Error;
            this.Connecting = this.Connection.Start();
            this.Connecting.ContinueWith( ( t ) =>
             {
                 if ( this.Connection.State == ConnectionState.Connected ) {
                    //接続完了通知
                    this.OnConnected();
                 }
             } );
        }

        public void Close()
        {
            this.Connection?.Stop();
        }

        public void Dispose()
        {
            this.Connection?.Dispose();
            this.Connecting?.Dispose();
        }

        private void Connection_Error( Exception obj )
        {
            throw obj;
        }

        private HubConnection CreateHubConnection()
        {
            return new HubConnection( this.ServerURL ?? throw new NullReferenceException( nameof( this.ServerURL ) ) );
        }

        public event Action<bool> HasConnectionChanged;

        public event Action<string> ServerURLChanged;

        public event Action Connected;

        protected virtual void OnHasConnectionChanged( bool args )
        {
            this.HasConnectionChanged?.Invoke( args );
        }

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