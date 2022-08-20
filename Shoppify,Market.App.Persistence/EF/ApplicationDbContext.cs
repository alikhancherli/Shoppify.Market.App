using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shoppify.Market.App.Domain.Entites;
using Shoppify.Market.App.Domain.Extensions;
using System.Reflection;

namespace Shoppify.Market.App.Persistence.EF
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            builder.RegisterAllEntities<IRootEntity>(typeof(IRootEntity).Assembly);
            builder.AddDefaultValueSqlConvention("Id", typeof(Guid), "NEWSEQUENTIALID()");
        }

        public override int SaveChanges()
        {
            CleanString();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            CleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            CleanString();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            CleanString();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void CleanString()
        {
            // Get entities that are in added or modified state
            var changedEntities = ChangeTracker.Entries().Where(_ => _.State == EntityState.Added || _.State == EntityState.Modified);

            foreach (var item in changedEntities)
            {
                if (item.Entity is null)
                    continue;

                // Get properties which are in type of string and public or instance bindig flag
                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(_ => _.CanRead && _.CanWrite && _.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    // Get value of the property
                    var propVal = property.GetValue(item.Entity, null)?.ToString();

                    if (propVal!.HasValue())
                    {
                        // Purging the value to get new cleaned value
                        var newVal = propVal?.Fa2En().FixPersianChars();
                        if (newVal == propVal)
                            continue;

                        property.SetValue(item.Entity, newVal);
                    }
                }
            }
        }
    }
}
