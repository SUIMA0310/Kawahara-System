using Prism.Modularity;
using Prism.Regions;
using System;
using DryIoc;
using Prism.DryIoc;
using DesktopApp.Services;

namespace DesktopApp
{
    public class SignalRModule : IModule
    {

        /// <summary>
        /// Applicationで使用するメインのDIコンテナ
        /// </summary>
        private IContainer Container { get; }

        public SignalRModule(IContainer container)
        {
            this.Container = container;
        }

        public void Initialize()
        {
            this.Container.Register<IConnectionService, ConnectionService>(Reuse.Singleton);
            this.Container.Register<IReactionHubProxy, ReactionHubProxy>(Reuse.Singleton);
        }
    }
}