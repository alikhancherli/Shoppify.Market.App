using Microsoft.AspNetCore.Mvc;

namespace Shoppify.Market.App.Infrastructure.ResultConfiguration
{
    public class ValidationErrorResult : IActionResult
    {
        public IEnumerable<string> errors { get; }

        public ValidationErrorResult(IEnumerable<string> errors)
        {
            this.errors = errors;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            ObjectResult objectResult = new ObjectResult(new { errors = errors})
            {
                StatusCode = 422
            };
            await objectResult.ExecuteResultAsync(context);
        }
    }
}
