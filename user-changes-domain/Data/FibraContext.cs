using Microsoft.EntityFrameworkCore;
using System.Linq;
using user_changes_domain.Data.Entities;

namespace user_changes_domain.Data
{
    public sealed class FibraContext : DbContext
    {

        public FibraContext(DbContextOptions<FibraContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public DbSet<Titulo> Titulos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                    e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FibraContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
