using DesktopApp.Views;
using DryIoc;
using Prism.DryIoc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopApp.Services;

namespace DesktopApp
{
    public class OverlayModule : IModule
    {

        private readonly IContainer Container;
        private readonly IRegionManager RegionManager;

        public OverlayModule(IContainer container, IRegionManager regionManager)
        {
            this.Container = container;
            this.RegionManager = regionManager;
        }

        public void Initialize()
        {
            this.Container.Register<IWindowService, OverlayWindowService>(Reuse.Singleton);

            this.Container.RegisterTypeForNavigation<OverlayControlView>();

            this.RegionManager.RegisterViewWithRegion("ContentRegion", typeof(OverlayControlView));
        }

    }
}
