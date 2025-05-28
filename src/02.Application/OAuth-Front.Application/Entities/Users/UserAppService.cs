using OAuth_Front.Application.Entities.Users.Contracts;
using OAuth_Front.Application.Entities.Users.Contracts.Dtos;
using OAuth_Front.Dtos;
using System.Net.Http.Headers;
using System.Text.Json;

namespace OAuth_Front.Application.Entities.Users;

public class UserAppService : IUserService
{


    public UserAppService()
    {
    }

    public async Task<ApiResultDto<List<GetAllUserDto>>> GetAll(string token)
    {
        using var client = new HttpClient();
        if (!string.IsNullOrEmpty(token))
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync("http://oauth.rdehghai.ir/api/users/all");
        var result = new ApiResultDto<List<GetAllUserDto>>();
        var json = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            //result.Success = true;
            //try
            //{
            //result.Data  = JsonSerializer.Deserialize<List<GetAllUserDto>>(json);

            //}
            //catch (Exception ex)
            //{

            //    throw new Exception(ex.Message);
            //}

            result.Error = "error";
            result.StatusCode = 400;
            result.Success = false;
        }
        else
        {
            result.Success = false;
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (root.TryGetProperty("error", out var errorProp))
                result.Error = errorProp.GetString();

            if (root.TryGetProperty("description", out var descProp))
                result.Description = descProp.GetString();
        }
        return result;
    }
}
