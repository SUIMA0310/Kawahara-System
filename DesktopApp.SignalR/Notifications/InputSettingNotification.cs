using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Interactivity.InteractionRequest;

namespace DesktopApp.Notifications
{
    public class InputSettingNotification : Confirmation
    {
        public string InputServerURL { get; set; }
        public string InputPresentationID { get; set; }
    }
}
