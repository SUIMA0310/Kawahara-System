using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute( typeof( AppServer.Startup ) )]

namespace AppServer
{
    public partial class Startup
    {
        public void Configuration( IAppBuilder app )
        {
            ConfigureAuth( app );
            app.MapSignalR();
        }
    }
}