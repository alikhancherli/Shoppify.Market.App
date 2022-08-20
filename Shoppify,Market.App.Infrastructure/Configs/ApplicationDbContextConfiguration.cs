using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shoppify.Market.App.Persistence;
using Shoppify.Market.App.Persistence.EF;

namespace Shoppify.Market.App.Infrastructure.Configs
{
    public static class ApplicationDbContextConfiguration
    {
        public static async Task ApplyDbChanges(this IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await db.Database.MigrateAsync();

            var dataInitializers = scope.ServiceProvider.GetServices<ISeed>();

            foreach (var item in dataInitializers)
                await item.Seed();
        }
    }
}
