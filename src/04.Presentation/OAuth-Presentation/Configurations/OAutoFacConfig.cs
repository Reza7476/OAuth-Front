using Autofac;
using Autofac.Extensions.DependencyInjection;
using OAuth_Front.Application.Configurations;
using OAuth_Front.Application.Entities.Users.Contracts;
using OAuth_Front.Interfaces;
using OAuth_Presentation.Services;

namespace OAuth_Presentation.Configurations;

public static class OAutoFacConfig
{
    public static ConfigureHostBuilder AddAutofac(this ConfigureHostBuilder builder)
    {
        builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.ConfigureContainer<ContainerBuilder>(option =>
        {
            option.RegisterModule(new AutofacModule());
        });

        return builder;
    }
}

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder _)
    {

        var assembly = System.Reflection.Assembly.GetAssembly(typeof(IUserService));
        if (assembly != null)
        {
            var serviceTypes = assembly.GetTypes()
                .Where(t => typeof(IService)
                .IsAssignableFrom(t) && t.IsInterface && t != typeof(IService))
                .ToList();

            foreach (var serviceType in serviceTypes)
            {
                var implementationType = assembly.GetTypes()
                    .FirstOrDefault(t => !t.IsInterface && serviceType.IsAssignableFrom(t));
                if (implementationType != null)
                {
                    _.RegisterType(implementationType)
                        .As(serviceType)
                        .InstancePerLifetimeScope();
                }
            }
        }

        _.RegisterType<HttpClientService>()
                .As<IHttpClientService>()
                .InstancePerLifetimeScope();


        _.Register(ctx =>
        {
            var clientFactory = ctx.Resolve<IHttpClientFactory>();
            return clientFactory.CreateClient();
        }).As<HttpClient>().InstancePerLifetimeScope();

   
    }
}
