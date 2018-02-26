namespace DesktopApp.Services
{
    public enum eStatusMessages
    {

        Ready,
        processing,


    }

    public static class StatusMessagesExpansion
    {

        private static readonly string[] _names =
        {
            "準備完了",
            "しばらくお待ちください"
        };

        public static string DisplayName(this eStatusMessages message)
        {
            return _names[(int)message];
        }

    }

}
