using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Overlay.Draw.Models
{
    public interface IReactionInteraction
    {

        event Action<DesktopApp.Models.eReactionType, DesktopApp.Models.Color> Interaction;

    }
}
