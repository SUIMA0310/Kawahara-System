using System.Windows;

using DesktopApp.Behaviors;
using DesktopApp.Views;

using DryIoc;

using Prism.DryIoc;
using Prism.Modularity;
using Prism.Regions;

namespace DesktopApp
{
    /// <summary>
    /// アプリケーションの起動処理
    /// </summary>
    public class Bootstrapper : DryIocBootstrapper
    {
        /// <summary>
        /// MainWindowの生成
        /// </summary>
        /// <returns>Windowのインスタンス</returns>
        protected override DependencyObject CreateShell()
        {
            return this.Container.Resolve<Shell>();
        }

        /// <summary>
        /// MainWindowの初期化（表示）
        /// </summary>
        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Moduleの登録
        /// </summary>
        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)this.ModuleCatalog;

            moduleCatalog.AddModule( typeof( CoreModule ) );
            moduleCatalog.AddModule( typeof( MainModule ) );
            moduleCatalog.AddModule( typeof( SignalRModule ) );
            moduleCatalog.AddModule( typeof( OverlayModule ) );
            moduleCatalog.AddModule( typeof( ControlModule ) );
        }

        /// <summary>
        /// RegionBehaviorを追加する
        /// </summary>
        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var factory = base.ConfigureDefaultRegionBehaviors();

            factory.AddIfMissing( nameof( DisposeViewModelsBehavior ), typeof( DisposeViewModelsBehavior ) );

            return factory;
        }
    }
}