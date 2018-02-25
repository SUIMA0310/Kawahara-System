using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AppServer.Helpers
{
    internal static class DbEntityExtension
    {
        public static DbPropertyEntry SafeGetProperty(this DbEntityEntry entry, string propertyName)
        {
            if (entry.CurrentValues.PropertyNames.Contains( propertyName )) {
                return entry.Property( propertyName );
            }

            return null;
        }
    }

}