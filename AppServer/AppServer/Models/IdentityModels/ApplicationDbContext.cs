using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using AppServer.Helpers;

namespace AppServer.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Presentation> Presentations { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var utcNow = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries().Where( x => x.State == EntityState.Added || x.State == EntityState.Modified )) {
                var createdOn = entry.SafeGetProperty( "CreateDateTime" );
                var updatedOn = entry.SafeGetProperty( "UpdateDateTime" );

                if (entry.State == EntityState.Added && createdOn != null) {
                    createdOn.CurrentValue = utcNow;
                }

                if (updatedOn != null) {
                    updatedOn.CurrentValue = utcNow;
                }
            }

            return base.SaveChangesAsync( cancellationToken );
        }

    }

}