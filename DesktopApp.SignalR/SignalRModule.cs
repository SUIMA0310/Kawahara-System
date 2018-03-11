using DesktopApp.Services;
using DesktopApp.Views;

using DryIoc;

using Prism.DryIoc;
using Prism.Modularity;
using Prism.Regions;

namespace DesktopApp
{
    public class SignalRModule : IModule
    {
        /// <summary>
        /// Applicationで使用するメインのDIコンテナ
        /// </summary>
        private readonly IContainer Container;

        /// <summary>
        /// Applicationで使用するメインのRegionマネージャ
        /// </summary>
        private readonly IRegionManager RegionManager;

        public SignalRModule( IContainer container, IRegionManager regionManage )
        {
            this.Container = container;
            this.RegionManager = regionManage;
        }

        public void Initialize()
        {
            this.Container.Register<IConnectionService, ConnectionService>( Reuse.Singleton );
            this.Container.Register<IReactionHubProxy, ReactionHubProxy>( Reuse.Singleton );

            this.Container.RegisterTypeForNavigation<ConnectionControlView>();

            this.RegionManager.RegisterViewWithRegion( "ContentRegion", typeof( ConnectionControlView ) );
        }
    }
}