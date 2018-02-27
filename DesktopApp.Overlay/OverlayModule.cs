using DryIoc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp
{
    public class OverlayModule : IModule
    {

        private IContainer Container { get; }
        private IRegionManager RegionManager { get; }

        public OverlayModule(IContainer container, IRegionManager regionManager)
        {
            this.Container = container;
            this.RegionManager = regionManager;
        }

        public void Initialize()
        {
        }

    }
}
