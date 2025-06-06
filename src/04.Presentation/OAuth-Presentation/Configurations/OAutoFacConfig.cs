using Autofac;
using Autofac.Extensions.DependencyInjection;
using OAuth_Presentation.Configurations.Interfaces;

namespace OAuth_Presentation.Configurations;

public static class OAutoFacConfig
{
    public static ConfigureHostBuilder AddAutofac(this ConfigureHostBuilder builder,string baseAddress)
    {
        builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.ConfigureContainer<ContainerBuilder>(option =>
        {
            option.RegisterModule(new AutofacModule(baseAddress));
        });

        return builder;
    }
}

public class AutofacModule : Module
{
    private readonly string _baseAddress;

    public AutofacModule(string baseAddress)
    {
        _baseAddress = baseAddress;
    }

    protected override void Load(ContainerBuilder _)
    {
        var baseAddress = _baseAddress;
        var assembly = System.Reflection.Assembly.GetAssembly(typeof(OAutoFacConfig));

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
                        .WithParameter("baseAddress", baseAddress)
                        .InstancePerLifetimeScope();
                }
            }
        }




        _.Register(ctx =>
        {
            var clientFactory = ctx.Resolve<IHttpClientFactory>();
            return clientFactory.CreateClient();
        }).As<HttpClient>().InstancePerLifetimeScope();


    }
}
