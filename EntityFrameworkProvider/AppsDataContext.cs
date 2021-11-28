using GoogleApps.Interfaces.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProvider
{
    public class AppsDataContext : DbContext
    {
        public DbSet<App> Apps { get; set; }
        public AppsDataContext(DbContextOptions<AppsDataContext> options) : base(options) { }
    }
}
