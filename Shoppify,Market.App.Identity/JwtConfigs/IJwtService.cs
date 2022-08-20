using Shoppify.Market.App.Domain.Entites;
using Shoppify.Market.App.Identity.Models;

namespace Shoppify.Market.App.Identity.JwtConfigs
{
    public interface IJwtService
    {
        Task<AccessTokenModel> GenerateTokenAsync(User user);
    }
}
