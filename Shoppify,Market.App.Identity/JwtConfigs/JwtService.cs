
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shoppify.Market.App.Domain.Entites;
using Shoppify.Market.App.Domain.Markers;
using Shoppify.Market.App.Identity.Models;
using Shoppify.Market.App.Service.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shoppify.Market.App.Identity.JwtConfigs
{
    public class JwtService : IJwtService, IScopedDependency
    {
        private readonly SignInManager<User> _signInManager;
        private ApplicationOptions _applicationOptions;

        public JwtService(
            SignInManager<User> signInManager,
            IOptionsMonitor<ApplicationOptions> applicationOptionsMonitor)
        {
            ArgumentNullException.ThrowIfNull(signInManager, nameof(signInManager));
            _signInManager = signInManager;

            _applicationOptions = applicationOptionsMonitor.CurrentValue;
            applicationOptionsMonitor.OnChange(_ => _applicationOptions = _);
        }

        public async Task<AccessTokenModel> GenerateTokenAsync(User user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_applicationOptions.JwtConfig.SecretKey);
            var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionKey = Encoding.UTF8.GetBytes(_applicationOptions.JwtConfig.EncryptKey);
            var encryptionCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = await GetUserClaims(user);

            var descriptor = new SecurityTokenDescriptor()
            {
                SigningCredentials = signinCredentials,
                EncryptingCredentials = encryptionCredentials,
                Audience = _applicationOptions.JwtConfig.Audience,
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Issuer = _applicationOptions.JwtConfig.Issuer,
                NotBefore = DateTime.UtcNow.AddMinutes(_applicationOptions.JwtConfig.NotBeforeMinutes),
                Expires = DateTime.UtcNow.AddMinutes(_applicationOptions.JwtConfig.ExpirationMinutes)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);
            var tmeSpn = securityToken.ValidTo - DateTime.UtcNow;
            return new AccessTokenModel()
            {
                AccessToken = tokenHandler.WriteToken(securityToken),
                ExpirationTime = securityToken.ValidTo.ToLocalTime()
            };
        }

        private async Task<IEnumerable<Claim>> GetUserClaims(User user)
        {
            var userClaims = await _signInManager.ClaimsFactory.CreateAsync(user);
            var claimsList = new List<Claim>(userClaims.Claims);
            claimsList.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? "09120000000"));

            return claimsList;
        }
    }
}
