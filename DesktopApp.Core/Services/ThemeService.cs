using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroRadiance.UI;

namespace DesktopApp.Services
{
    public class ThemeService : IThemeService
    {

        public bool IsBusy
        {
            get => MetroRadiance.UI.ThemeService.Current.Accent == Accent.Orange;
            set {
                if ( this.IsBusy == value ) { return; }
                if (value) {
                    MetroRadiance.UI.ThemeService.Current.ChangeAccent( Accent.Orange );
                } else {
                    MetroRadiance.UI.ThemeService.Current.ChangeAccent( Accent.Blue );
                }
            }
        }

    }
}
