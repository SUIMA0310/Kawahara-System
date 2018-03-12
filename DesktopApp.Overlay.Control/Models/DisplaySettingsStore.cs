using DesktopApp.Overlay.Control.Properties;

namespace DesktopApp.Models
{
    public class DisplaySettingsStore : IDisplaySettingsStore
    {
        public float MaxOpacity
        {
            get => Settings.Default.MaxOpacity;
            set => Settings.Default.MaxOpacity = value;
        }

        public float Scale
        {
            get => Settings.Default.Scale;
            set => Settings.Default.Scale = value;
        }

        public float DisplayTime
        {
            get => Settings.Default.DisplayTime;
            set => Settings.Default.DisplayTime = value;
        }

        public string MoveMethodName
        {
            get;
            set;
        }

        public string OpacityCurveName
        {
            get;
            set;
        }

        public void Save()
        {
            Settings.Default.Save();
        }
    }
}