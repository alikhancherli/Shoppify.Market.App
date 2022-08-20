using Microsoft.AspNetCore.Mvc;
using Shoppify.Market.App.Service.Handlers;

namespace Shoppify.Market.App.Infrastructure.ResultConfiguration
{
    public class ApiController : ControllerBase
    {
        [NonAction]
        public virtual IActionResult HandlerResultExcecution(AppHandlerResult appHandlerResult)
        {
            if (appHandlerResult.Succeeded)
            {
                if (appHandlerResult.Data is null)
                    return Ok();
                else
                    return Ok(appHandlerResult.Data);
            }
            else if (appHandlerResult.InvalidProcess)
                return new ValidationErrorResult(appHandlerResult.Errors ?? new[] { "invalid request" });

            return Failed();
        }

        [NonAction]
        public IActionResult Failed()
        {
            return StatusCode(500);
        }
    }
}
