using Microsoft.AspNetCore.Identity;
using Shoppify.Market.App.Domain.Entites;
using Shoppify.Market.App.Domain.ValueTypes.EnumTypes;

namespace Shoppify.Market.App.Domain.Entites
{
    public class User : IdentityUser<int>, IRootEntity
    {
        public string FullName { get; init; } = "بدون نام";
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public bool IsActive { get; init; } = true;
        public DateTimeOffset? LastLoginDate { get; set; }
    }
}
