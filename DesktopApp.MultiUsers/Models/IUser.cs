namespace DesktopApp.MultiUsers.Models
{
    public interface IUser
    {
        string Name    { get; set; }
        uint GoodCount { get; set; }
        uint NiceCount { get; set; }
        uint FunCount  { get; set; }
    }
}