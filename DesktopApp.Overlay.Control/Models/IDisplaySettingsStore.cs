namespace DesktopApp.Models
{
    public interface IDisplaySettingsStore
    {
        float DisplayTime { get; set; }
        float MaxOpacity { get; set; }
        float Scale { get; set; }

        void Save();
    }
}