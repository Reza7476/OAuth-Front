namespace OAuth_Presentation.Configurations;

public static class HttpClientConfiguration
{
    public static IServiceCollection AddCustomHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("ApiClient", (serviceProvider, client) =>
        {
            var config = configuration.GetSection("ApiSettings");
            client.BaseAddress = new Uri(config["BaseUrl"]!);
        }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        });

        return services;
    }
}
