using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesktopApp.ViewModels
{
    public class OverlayControlViewModel : ViewModelBase
    {
        public OverlayControlViewModel()
        {
            this.Title.Value = "Overlay";
        }
    }
}
