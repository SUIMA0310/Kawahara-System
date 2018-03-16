using DesktopApp.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using DryIoc;
using Prism.DryIoc;
using DesktopApp.Services;
using DesktopApp.MultiUsers.Models;

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
            this.Container.Register<IUsersActivity, UsersActivity>( Reuse.Singleton );
            this.Container.Register<IUsersStore, UsersStore>( Reuse.Singleton );

            this.Container.RegisterTypeForNavigation<MultiUsersView>();

            this.RegionManager.RegisterViewWithRegion( "ContentRegion", typeof( MultiUsersView ) );
        }
    }
}