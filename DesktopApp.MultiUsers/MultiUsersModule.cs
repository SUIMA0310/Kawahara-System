using DesktopApp.MultiUsers.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using DryIoc;
using Prism.DryIoc;

namespace DesktopApp
{
    public class MultiUsersModule : IModule
    {
        private IRegionManager RegionManager;
        private IContainer Container;

        public MultiUsersModule(IContainer container, IRegionManager regionManager)
        {
            Container = container;
            RegionManager = regionManager;
        }

        public void Initialize()
        {
            this.Container.RegisterTypeForNavigation<MultiUsersView>();

            this.RegionManager.RegisterViewWithRegion( "ContentRegion", typeof( MultiUsersView ) );
        }
    }
}