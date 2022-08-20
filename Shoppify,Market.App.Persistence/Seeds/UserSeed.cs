using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shoppify.Market.App.Domain.Constants;
using Shoppify.Market.App.Domain.Entites;
using Shoppify.Market.App.Domain.ValueTypes.EnumTypes;

namespace Shoppify.Market.App.Persistence.Seeds
{
    public sealed class UserSeed : ISeed
    {
        private readonly UserManager<User> _userManager;

        public UserSeed(UserManager<User> userManager)
        {
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));
            _userManager = userManager;
        }

        public async Task Seed()
        {
            if (!await _userManager.Users.AnyAsync(_ => _.UserName.ToLower() == ApplicationUser.DefaultAppUser))
            {
                var user = new User()
                {
                    UserName = ApplicationUser.DefaultAppUser,
                    FullName = "ادمین کل سیستم",
                    Age = 23,
                    Email = "example@email.com",
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                var result = await _userManager.CreateAsync(user, "admin12345678");

                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(user, AppRoles.Admin.ToString());
            }
        }
    }
}
