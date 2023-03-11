using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using LiteX.Storage.FileSystem;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shoppify.Market.App.Identity.ExtensionMethods;
using Shoppify.Market.App.Infrastructure.Configs;
using Shoppify.Market.App.Infrastructure.ResultConfiguration;
using Shoppify.Market.App.Infrastructure.Swagger;
using Shoppify.Market.App.Persistence.EF;
using Shoppify.Market.App.Service.Commands.ProductCategories;
using Shoppify.Market.App.Service.Handlers;
using Shoppify.Market.App.Service.Options;
using Shoppify.Market.App.Service.Resources;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.WebHost.UseUrls("http://+:5107", "https://+:7107");
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Configuration.AddJsonFile("./values.json");

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AppHandlerResult).Assembly, typeof(ApplicationDbContextConfiguration).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(typeof(GlobalResources).Assembly, Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssemblies(new[] { typeof(AddProductCategoryCommand).Assembly });
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddSwaggerAsCustom();
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddCustomAuthentication();

builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection(nameof(ApplicationOptions)));
builder.Services.Configure<GlobalResources>(builder.Configuration.GetSection(nameof(GlobalResources)));
builder.Services.Configure<LiteXOptons>(builder.Configuration.GetSection(nameof(LiteXOptons)));

builder.Services.AddLiteXFileSystemStorageServiceFactory(opt =>
{
    LiteXOptons liteXOptons = new LiteXOptons();
    builder.Configuration.GetSection(nameof(LiteXOptons)).Bind(liteXOptons);
    opt.Directory = liteXOptons.Directory;
    opt.EnableLogging = true;
}, "MyProvider");
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ApiDocumentation>();
builder.Services.AddJwtSetting(builder.Configuration.GetSection(nameof(ApplicationOptions)).Get<ApplicationOptions>());
builder.Services.AddCustomPolicies();
builder.Services.AddMediatR(typeof(ApplicationOptions).Assembly);
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacExtensions()));

var app = builder.Build();

await app.ApplyDbChanges();
app.UseMiddleware<ExceptionHandlerMiddleWare>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCustomSwagger();
    app.UseSwaggerUI(opt =>
    {
        var scope = app.Services.CreateScope();
        var apiDescriptors = scope.ServiceProvider.GetRequiredService<IApiVersionDescriptionProvider>();


        foreach (var desc in apiDescriptors.ApiVersionDescriptions)
        {
            opt.DefaultModelsExpandDepth(-1);
            opt.SwaggerEndpoint($"../swagger/{desc.GroupName}/swagger.json", desc.ApiVersion.ToString());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
