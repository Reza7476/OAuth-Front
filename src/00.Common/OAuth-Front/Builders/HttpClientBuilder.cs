using System.Net.Http.Headers;

namespace OAuth_Front.Builders;

public class HttpClientBuilder
{
    private readonly HttpClient _httpClient;

    public HttpClientBuilder()
    {
        _httpClient = new HttpClient();
    }

    public HttpClientBuilder WithToken(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new 
            
            AuthenticationHeaderValue("Bearer", token);
        return this;
    }

    public HttpClient Build() 
    {
        return _httpClient;
    }
    

}
