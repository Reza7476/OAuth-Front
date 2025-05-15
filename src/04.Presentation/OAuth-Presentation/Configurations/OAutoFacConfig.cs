using Autofac.Extensions.DependencyInjection;
using Autofac;
using System.Runtime.CompilerServices;

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
    protected override void Load(ContainerBuilder container)
    {
        base.Load(container);
    }
}
