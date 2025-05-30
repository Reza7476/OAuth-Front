using OAuth_Front.Application.Entities.Users.Contracts.Dtos;
using OAuth_Front.Builders;
using OAuth_Front.Dtos;
using System.Net.Http.Headers;

namespace OAuth_Presentation.Services.Users;

public class UserService : IUserService
{

    private readonly HttpClient _client;

    public UserService(HttpClient client, string? baseAddress=null )
    {
        _client = client;
        _client.BaseAddress=new Uri(baseAddress!);
    }

    public async Task<ApiResultDto<List<GetAllUserDto>>> GetAll(string? token)
    {

        _client.DefaultRequestHeaders.Authorization = new
            AuthenticationHeaderValue("Bearer", token);

        var response = await _client.GetAsync("users/all");
        var resultContent = await response.Content.ReadAsStringAsync();
        var result= new ApiResultDto<List<GetAllUserDto>>();
        return null;

    }
}
