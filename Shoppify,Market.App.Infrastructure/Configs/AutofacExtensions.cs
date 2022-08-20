using Autofac;
using Shoppify.Market.App.Domain.Entites;
using Shoppify.Market.App.Domain.Markers;
using Shoppify.Market.App.Identity.JwtConfigs;
using Shoppify.Market.App.Persistence.EF;
using Shoppify.Market.App.Persistence.Repositories.Contracts;
using Shoppify.Market.App.Persistence.Repositories.Implementations;
using Shoppify.Market.App.Service.Options;

namespace Shoppify.Market.App.Infrastructure.Configs
{
    public class AutofacExtensions : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));

            var domainAssembly = typeof(IRootEntity).Assembly;
            var persistenceAssembly = typeof(ApplicationDbContext).Assembly;
            var serviceAssembly = typeof(ApplicationOptions).Assembly;
            var infraAssembly = typeof(AutofacExtensions).Assembly;
            var identityAssembly = typeof(IJwtService).Assembly;

            builder.RegisterAssemblyTypes(domainAssembly, persistenceAssembly, serviceAssembly, infraAssembly, identityAssembly)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(domainAssembly, persistenceAssembly, serviceAssembly, infraAssembly, identityAssembly)
                .AssignableTo<ITransientDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(domainAssembly, persistenceAssembly, serviceAssembly, infraAssembly, identityAssembly)
                .AssignableTo<ISingletonDependency>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
