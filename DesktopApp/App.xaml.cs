using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public DryIoc.IContainer Container { get; protected set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
            this.Container = bootstrapper.Container;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.Container.Dispose();
            base.OnExit(e);
        }
    }
}
