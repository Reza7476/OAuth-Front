using OAuth_Presentation.Models.Commons;
using OAuth_Presentation.Models.Users;
using System.Net.Http.Headers;
using System.Text.Json;

namespace OAuth_Presentation.Services.Users;

public class UserService : IUserService
{

    private readonly HttpClient _client;

    public UserService(HttpClient client, string? baseAddress = null)
    {
        _client = client;
        _client.BaseAddress = new Uri(baseAddress!);
    }

    public async Task<ApiResultDto<List<GetUserDto>>> GetAll(string? token)
    {
        _client.DefaultRequestHeaders.Authorization = new
            AuthenticationHeaderValue("Bearer", token);
        var response = await _client.GetAsync("users/all");
        var resultContent = await response.Content.ReadAsStringAsync();
        var result = new ApiResultDto<List<GetUserDto>>();
        if (response.IsSuccessStatusCode)
        {
            var users = JsonSerializer.Deserialize<List<GetUserDto>>(resultContent);
            result.Data = users!;
            result.StatusCode = (int)response.StatusCode;
            result.IsSuccess = response.IsSuccessStatusCode;
        }
        else
        {
            result.Error = resultContent;
            result.StatusCode = (int)response.StatusCode;
            result.IsSuccess = response.IsSuccessStatusCode;
        }

        return result;
    }
}
