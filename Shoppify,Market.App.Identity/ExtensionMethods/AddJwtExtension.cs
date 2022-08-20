using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shoppify.Market.App.Domain.Exceptions;
using Shoppify.Market.App.Service.Options;
using System.Text;

namespace Shoppify.Market.App.Identity.ExtensionMethods
{
    public static class AddJwtExtension
    {
        public static void AddJwtSetting(this IServiceCollection services, ApplicationOptions applicationOptions)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    RequireSignedTokens = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(applicationOptions.JwtConfig.SecretKey)),
                    TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(applicationOptions.JwtConfig.EncryptKey)),
                    ValidateIssuer = true,
                    ValidIssuer = applicationOptions.JwtConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudience = applicationOptions.JwtConfig.Audience
                };
            });
        }
    }
}
