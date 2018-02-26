using DesktopApp.SignalR.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using DryIoc;
using Prism.DryIoc;

namespace DesktopApp.SignalR
{
    public class SignalRModule : IModule
    {
        private IRegionManager _regionManager;
        private IContainer _container;

        public SignalRModule(IContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<ViewA>();
        }
    }
}