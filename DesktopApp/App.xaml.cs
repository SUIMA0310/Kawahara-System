using System.Windows;
using MetroRadiance.UI;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public DryIoc.IContainer Container { get; protected set; }

        protected override void OnStartup( StartupEventArgs e )
        {
            base.OnStartup( e );

            ThemeService.Current.Register( this, Theme.Dark, Accent.Blue );

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
            this.Container = bootstrapper.Container;
        }

        protected override void OnExit( ExitEventArgs e )
        {
            this.Container.Dispose();
            base.OnExit( e );
        }
    }
}