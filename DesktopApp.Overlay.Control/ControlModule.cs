using Prism.Modularity;
using Prism.Regions;
using System;
using DryIoc;
using Prism.DryIoc;

namespace DesktopApp
{
    public class ControlModule : IModule
    {
        private IRegionManager RegionManager;
        private IContainer Container;

        public ControlModule(IContainer container, IRegionManager regionManager)
        {
            this.Container = container;
            this.RegionManager = regionManager;
        }

        public void Initialize()
        {
            
        }
    }
}