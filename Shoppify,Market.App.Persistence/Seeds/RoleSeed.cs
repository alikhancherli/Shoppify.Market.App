using Microsoft.AspNetCore.Identity;
using Shoppify.Market.App.Domain.Entites;
using Shoppify.Market.App.Domain.ValueTypes.EnumTypes;

namespace Shoppify.Market.App.Persistence.Seeds
{
    public sealed class RoleSeed : ISeed
    {

        private readonly RoleManager<Role> _roleManager;

        public RoleSeed(RoleManager<Role> roleManager)
        {
            ArgumentNullException.ThrowIfNull(typeof(RoleManager<Role>), nameof(roleManager));
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            string[] roles = Enum.GetNames(typeof(AppRoles));

            foreach (var item in roles)
                if (!await _roleManager.RoleExistsAsync(item))
                    await _roleManager.CreateAsync(new Role()
                    {
                        Name = item,
                        NormalizedName = item.Trim().ToUpper()
                    });
        }
    }
}
