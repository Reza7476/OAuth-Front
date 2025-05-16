using OAuth_Front.Application.Configurations;
using OAuth_Front.Application.Entities.Users.Contracts;
using OAuth_Front.Application.Entities.Users.Contracts.Dtos;
using System.Net.Http.Json;

namespace OAuth_Front.Application.Entities.Users;

public class UserAppService : IUserService
{
    private readonly IHttpClientService _httpClientService;

    public UserAppService(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    public async Task<List<GetAllUserDto>> GetAll()
    {
        var response = await _httpClientService.GetAsync("users/all");
        response.EnsureSuccessStatusCode();
        var users = await response.Content.ReadFromJsonAsync<List<GetAllUserDto>>();
        return users ?? new List<GetAllUserDto>();
    }
}
