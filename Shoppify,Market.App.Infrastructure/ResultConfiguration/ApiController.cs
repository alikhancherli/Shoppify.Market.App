using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoppify.Market.App.Service.Handlers;

namespace Shoppify.Market.App.Infrastructure.ResultConfiguration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ApiController : ControllerBase
    {
        [NonAction]
        public virtual ApplicationApiResult<object> HandlerResultExcecution(AppHandlerResult appHandlerResult)
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
        public ApplicationApiResult<object> Failed()
        {
            return new ObjectResult(null) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }
}
