using System;

namespace DesktopApp.Overlay.Draw.Models
{
    public interface IReactionInteraction
    {
        event Action<DesktopApp.Models.eReactionType, DesktopApp.Models.Color> Interaction;
    }
}