using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shoppify.Market.App.Infrastructure.Swagger
{
    public class UnauthorizedOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var controllerFilters = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            var controllerMetadta = context.ApiDescription.ActionDescriptor.EndpointMetadata;

            if (controllerFilters.Any(p => p.Filter is AuthorizeFilter) || controllerMetadta.Any(p => p is AuthorizeAttribute))
                operation.Responses.TryAdd("401", new OpenApiResponse() { Description = "Unauthorized" });
        }
    }
}
