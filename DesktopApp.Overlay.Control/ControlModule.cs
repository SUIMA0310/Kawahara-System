using Prism.Modularity;
using Prism.Regions;
using System;
using DryIoc;
using Prism.DryIoc;
using DesktopApp.Views;
using DesktopApp.Models;
using DesktopApp.Services;

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
            this.Container.Register<IDisplaySettingsStore, DisplaySettingsStore>(Reuse.Singleton);
            this.Container.Register<IDisplayControlService, DisplayControlService>(Reuse.Singleton);

            this.Container.RegisterTypeForNavigation<OverlayShownControlView>();

            this.RegionManager.RegisterViewWithRegion("ContentRegion", typeof(OverlayShownControlView));
        }
    }
}