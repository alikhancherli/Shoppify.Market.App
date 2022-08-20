using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Shoppify.Market.App.Identity.ExtensionMethods;
using Shoppify.Market.App.Infrastructure.Configs;
using Shoppify.Market.App.Infrastructure.Swagger;
using Shoppify.Market.App.Persistence.EF;
using Shoppify.Market.App.Service.Commands.ProductCategories;
using Shoppify.Market.App.Service.Handlers;
using Shoppify.Market.App.Service.Options;
using Shoppify.Market.App.Service.Resources;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Configuration.AddJsonFile("./values.json");

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AppHandlerResult).Assembly, typeof(ApplicationDbContextConfiguration).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(typeof(GlobalResources).Assembly, Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssemblies(new[] { typeof(AddProductCategoryCommand).Assembly });
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });

    opt.OperationFilter<UnauthorizedOperationFilter>();
});
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddCustomAuthentication();

builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection(nameof(ApplicationOptions)));
builder.Services.Configure<GlobalResources>(builder.Configuration.GetSection(nameof(GlobalResources)));

builder.Services.AddJwtSetting(builder.Configuration.GetSection(nameof(ApplicationOptions)).Get<ApplicationOptions>());
builder.Services.AddMediatR(typeof(ApplicationOptions).Assembly);
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacExtensions()));

var app = builder.Build();

await app.ApplyDbChanges();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.DefaultModelsExpandDepth(-1);
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
