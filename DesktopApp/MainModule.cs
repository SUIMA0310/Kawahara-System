using DryIoc;
using Prism.Regions;
using Prism.DryIoc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopApp.Views;

namespace DesktopApp
{
    public class MainModule : IModule
    {

        private IContainer Container { get; }
        private IRegionManager RegionManager { get; }

        public MainModule(IContainer container, IRegionManager regionManager)
        {
            this.Container = container;
            this.RegionManager = regionManager;
        }

        public void Initialize()
        {
            this.Container.RegisterTypeForNavigation<StatusBarControl>();

            this.RegionManager.RegisterViewWithRegion("StatusBarRegion", typeof(StatusBarControl));
        }

    }
}
