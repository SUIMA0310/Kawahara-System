
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

        public abstract void Open();

    }
}
