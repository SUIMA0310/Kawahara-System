﻿
using Prism.Logging;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Services
{
    public abstract class HubProxyService : IHubProxyService
    {

        protected ILoggerFacade Logger { get; }
        protected IConnectionService Connection { get; }
        protected IHubProxy Proxy { get; private set; }

        public HubProxyService(ILoggerFacade logger, IConnectionService connection)
        {
            this.Logger = logger;
            this.Connection = connection ?? throw new ArgumentNullException( nameof( connection ) );
        }

        public virtual void Open()
        {
            this.Proxy = this.Connection.CreateHubProxy(this.HubName);
            this.PreInitializeProxy();
            this.Connection.Open();
            this.PostInitializeProxy();
        }

        protected abstract string HubName { get; }

        protected virtual void PreInitializeProxy() { }
        protected virtual void PostInitializeProxy() { }

    }
}