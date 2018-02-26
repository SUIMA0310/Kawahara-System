using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using DesktopApp.Services;
using DryIoc;

namespace DesktopApp
{
    public class CoreModule : IModule
    {

        private IContainer Container { get; }

        public CoreModule(IContainer container)
        {
            this.Container = container;
        }

        public void Initialize()
        {
            this.Container.Register<IStatusService, StatusService>(Reuse.Singleton);
        }

    }
}
