using OAuth_Front.Application.Register.Contracts;
using OAuth_Front.Application.Register.Contracts.Dtos;
using System.Net.Http.Json;

namespace OAuth_Front.Application.Register;

public class RegisterAppService : IRegisterService
{
    public  async Task<string> LogIn(LogInDto dto)
    {
        try
        {
            var site = dto.SiteAudience.ToString();
            dto.SiteAudience = "oauth.front.rdehghai.ir";
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            using var client = new HttpClient(handler);
            var response = await client.PostAsJsonAsync("https://oauth.rdehghai.ir/api/SignIn/login", dto);
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
