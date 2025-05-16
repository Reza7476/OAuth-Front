using OAuth_Front.Interfaces;

namespace OAuth_Front.Application.Configurations;

public interface IHttpClientService : IService
{
    Task<HttpResponseMessage> GetAsync(string requestUri);
}
