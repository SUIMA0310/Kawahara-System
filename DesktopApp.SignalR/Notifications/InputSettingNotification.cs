namespace DesktopApp.Notifications
{
    public class InputSettingNotification : IInputSettingNotification
    {
        public string InputServerURL
        {
            get => Properties.Settings.Default.LastServerURL;
            set => Properties.Settings.Default.LastServerURL = value;
        }

        public string InputPresentationID
        {
            get => Properties.Settings.Default.LastPresentationID;
            set => Properties.Settings.Default.LastPresentationID = value;
        }

        public bool Confirmed
        {
            get => this._Confirmed;
            set {
                if ( value ) {
                    Properties.Settings.Default.Save();
                }
                this._Confirmed = value;
            }
        }

        public string Title { get; set; }
        public object Content { get; set; }

        private bool _Confirmed = false;
    }
}