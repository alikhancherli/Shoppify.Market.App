using LiteX.Storage.FileSystem;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Security.Policy;

namespace Shoppify.Market.App.Infrastructure.ResultConfiguration
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly IWebHostEnvironment _environment;
        private readonly RequestDelegate _request;

        public ExceptionHandlerMiddleWare(IWebHostEnvironment environment, RequestDelegate request)
        {
            _environment = environment;
            _request = request;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            var jsonSetting = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var jsonMimeType = MimeInfo.GetMimeType("json");
            try
            {
                await _request(httpContext);
            }
            catch (UnauthorizedAccessException e)
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                httpContext.Response.ContentType = jsonMimeType;
                var result = new ApplicationApiResult(httpContext.Response.StatusCode, false, e.Message);
                var jsonResult = JsonConvert.SerializeObject(result, jsonSetting);
                await httpContext.Response.WriteAsync(jsonResult);
            }
            catch (Exception e)
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = jsonMimeType;
                ApplicationApiResult result = null;
                string jsonResult = "";
                if (!_environment.IsDevelopment())
                {
                    result = new ApplicationApiResult(httpContext.Response.StatusCode, false, e.Message);
                    jsonResult = JsonConvert.SerializeObject(result, jsonSetting);
                    await httpContext.Response.WriteAsync(jsonResult);
                }
                else
                {
                    result = new ApplicationApiResult<string>(e.StackTrace, httpContext.Response.StatusCode, false, e.Message);
                    jsonResult = JsonConvert.SerializeObject(result, jsonSetting);
                    await httpContext.Response.WriteAsync(jsonResult);
                }
            }
        }

    }
}
