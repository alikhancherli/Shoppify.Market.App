using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shoppify.Market.App.Infrastructure.Swagger
{
    public class ApiDocumentation : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

        public ApiDocumentation(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            _apiVersionDescriptionProvider = apiVersionDescriptionProvider ?? throw new ArgumentNullException(nameof(apiVersionDescriptionProvider));
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var desc in _apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(desc.GroupName, new OpenApiInfo()
                {
                    Title = "Api title here",
                    Version = desc.ApiVersion.ToString()
                });
            }
        }
    }
}
