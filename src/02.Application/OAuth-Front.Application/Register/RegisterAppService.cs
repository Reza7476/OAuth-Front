using OAuth_Front.Application.Register.Contracts;
using OAuth_Front.Application.Register.Contracts.Dtos;
using OAuth_Front.Dtos;
using System.Net.Http.Json;

namespace OAuth_Front.Application.Register;

public class RegisterAppService : IRegisterService
{
    public async Task<ApiResultDto<string>> LogIn(LogInDto dto)
    {
        try
        {
            var result = new ApiResultDto<string>();
            var site = dto.SiteAudience.ToString();
            dto.SiteAudience = "oauth.front.rdehghai.ir";
            using var client = new HttpClient();
            var response = await client.PostAsJsonAsync("https://oauth.rdehghai.ir/api/SignIn/login", dto);
            //var response = await client.PostAsJsonAsync("https://localhost:44345/api/SignIn/login", dto);
            result.StatusCode = (int)response.StatusCode;
            var resultContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                result.Data=resultContent;
                result.Success = true;
            }
            else
            {
                result.Success = false;
                using var doc = System.Text.Json.JsonDocument.Parse(resultContent);
                var root = doc.RootElement;

                if (root.TryGetProperty("error", out var errorProp))
                    result.Error = errorProp.GetString();

                if (root.TryGetProperty("description", out var descProp))
                    result.Description = descProp.GetString();
            }

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
