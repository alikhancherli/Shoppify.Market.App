using Microsoft.Extensions.DependencyInjection;
using Shoppify.Market.App.Domain.Entites;
using Shoppify.Market.App.Persistence.EF;

namespace Shoppify.Market.App.Identity.ExtensionMethods
{
    public static class AddAuthenticationExtenstion
    {
        public static void AddCustomAuthentication(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(opt =>
            {
                opt.SignIn.RequireConfirmedPhoneNumber = false;
                opt.SignIn.RequireConfirmedEmail = false;
                opt.SignIn.RequireConfirmedAccount = true;

                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();
        }
    }
}
