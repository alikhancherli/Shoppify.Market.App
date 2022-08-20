using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shoppify.Market.App.Domain.Entites;
using Shoppify.Market.App.Identity.JwtConfigs;
using System.ComponentModel.DataAnnotations;

namespace Shoppify.Market.App.Api.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;

        public AuthController(IJwtService jwtService, UserManager<User> userManager)
        {
            ArgumentNullException.ThrowIfNull(jwtService, nameof(jwtService));
            ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));
            _jwtService = jwtService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([Required] string username, [Required] string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (!await _userManager.CheckPasswordAsync(user, password))
                return BadRequest("پسورد یا نام کاربری نامعتبر است");

            var token = await _jwtService.GenerateTokenAsync(user);
            return Ok(token);
        }
    }
}
