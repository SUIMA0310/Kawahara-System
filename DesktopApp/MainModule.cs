using DesktopApp.Views;

using DryIoc;

using Prism.DryIoc;
using Prism.Modularity;
using Prism.Regions;

namespace DesktopApp
{
    public class MainModule : IModule
    {
        private IContainer Container { get; }
        private IRegionManager RegionManager { get; }

        public MainModule( IContainer container, IRegionManager regionManager )
        {
            this.Container = container;
            this.RegionManager = regionManager;
        }

        public void Initialize()
        {
            this.Container.RegisterTypeForNavigation<StatusBarControl>();

            this.RegionManager.RegisterViewWithRegion( "StatusBarRegion", typeof( StatusBarControl ) );
        }
    }
}