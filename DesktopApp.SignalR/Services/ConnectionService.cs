﻿using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private ILoggerFacade Logger { get; }

        private Task Connecting { get; set; }

        /// <summary>
        /// 現在接続を完了しているか
        /// </summary>
        public bool HasConnection => this.Connection?.State == ConnectionState.Connected;

        private HubConnection Connection { get; set; }

        public ConnectionService(ILoggerFacade logger)
        {

            this.Logger = logger;

        }

        public IHubProxy CreateHubProxy( string hubName )
        {
            if (this.Connection == null) { this.Connection = this.CreateHubConnection(); }
            return this.Connection.CreateHubProxy( hubName );
        }

        public void Open()
        {
            //2度目以降の実行を無視
            if ( this._IsOpened ) { return; }
            this._IsOpened = true;

            if ( this.Connection == null ) { this.Connection = this.CreateHubConnection(); }
            this.Connection.Error += this.Connection_Error;
            this.Connecting = this.Connection.Start();

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
        private void Connection_Error(Exception obj)
        {
            throw obj;
        }

        private HubConnection CreateHubConnection()
        {
            return new HubConnection(Properties.Settings.Default.ServerURL);
        }


    }
}