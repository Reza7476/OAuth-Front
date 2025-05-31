using Autofac;
using Autofac.Extensions.DependencyInjection;
using OAuth_Presentation.Configurations.Interfaces;

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
        var baseAddress = "Https://oauth.rdehghai.ir/api/";
        var assembly = System.Reflection.Assembly.GetAssembly(typeof(OAutoFacConfig));

        // _.RegisterType<HttpContextAccessor>()
        //   .As<IHttpContextAccessor>()
        //   .SingleInstance();
        // _.RegisterType<JwtTokenHandler>();

        //// ثبت HttpClient با Handler و baseAddress
        // _.Register(ctx =>
        // {
        //     var handler = ctx.Resolve<JwtTokenHandler>();
        //     var client = new HttpClient(handler)
        //     {
        //         BaseAddress = new Uri(baseAddress)
        //     };
        //     return client;
        // }).As<HttpClient>().InstancePerLifetimeScope();


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
