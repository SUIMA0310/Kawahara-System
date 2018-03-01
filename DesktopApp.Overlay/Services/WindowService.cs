using DesktopApp.Overlay.Core;
using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Services
{

    public class WindowService : WindowServiceBase<IWindowFactory, IWindowController>
    {

        

        
        

        

        

        protected override void OnWindowControllerChanged(EventArgs<IWindowController> eventArgs)
        {
            base.OnWindowControllerChanged(eventArgs);

            

        }

        public override void Dispose()
        {
            base.Dispose();

            

        }

    }
}
