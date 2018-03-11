using DesktopApp.Services;

using DryIoc;

using Prism.Modularity;

namespace DesktopApp
{
    public class CoreModule : IModule
    {
        private IContainer Container { get; }

        public CoreModule( IContainer container )
        {
            this.Container = container;
        }

        public void Initialize()
        {
            this.Container.Register<IStatusService, StatusService>( Reuse.Singleton );
        }
    }
}