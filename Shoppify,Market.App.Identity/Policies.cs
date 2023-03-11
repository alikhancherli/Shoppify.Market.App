using Microsoft.AspNetCore.Authorization;
using Shoppify.Market.App.Domain.ValueTypes.EnumTypes;

namespace Shoppify.Market.App.Identity
{
    public static class Policies
    {
        public const string AdminAccess = nameof(AdminAccess);

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole(AppRoles.Admin.ToString())
                .Build();
        }
    }
}
