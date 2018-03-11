using DesktopApp.Services;
using DesktopApp.Views;

using DryIoc;

using Prism.DryIoc;
using Prism.Modularity;
using Prism.Regions;

namespace DesktopApp
{
    public class OverlayModule : IModule
    {
        private readonly IContainer Container;
        private readonly IRegionManager RegionManager;

        public OverlayModule( IContainer container, IRegionManager regionManager )
        {
            this.Container = container;
            this.RegionManager = regionManager;
        }

        public void Initialize()
        {
            this.Container.Register<IOverlayWindowService, OverlayWindowService>( Reuse.Singleton );

            this.Container.RegisterTypeForNavigation<OverlayControlView>();
            this.Container.RegisterTypeForNavigation<OverlayWindow>();

            this.RegionManager.RegisterViewWithRegion( "ContentRegion", typeof( OverlayControlView ) );
        }
    }
}