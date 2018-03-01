using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Services
{
    public interface IOverlayWindowController : IWindowController
    {

        System.Windows.Forms.Screen UseScreen { get; set; }

    }
}
