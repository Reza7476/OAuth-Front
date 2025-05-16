using OAuth_Front.Application.Configurations;

namespace OAuth_Presentation.Services;

public class HttpClientService : IHttpClientService
{
    private readonly IHttpClientFactory _clientFactory;

    public HttpClientService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
    }

    public async Task<HttpResponseMessage> GetAsync(string requestUri)
    {
        var client = _clientFactory.CreateClient("ApiClient");
        return await client.GetAsync(requestUri);
    }
}
