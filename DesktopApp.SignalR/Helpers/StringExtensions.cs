namespace DesktopApp.Helpers
{
    public static class StringExtensions
    {
        public static string NotEmptyOrDefault( this string source, string def )
        {
            if ( string.IsNullOrWhiteSpace( source ) ) {
                return def;
            }
            return source;
        }
    }
}