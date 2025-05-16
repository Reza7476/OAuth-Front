using OAuth_Front.Application.Configurations;
using OAuth_Front.Application.Entities.Users.Contracts;
using OAuth_Front.Application.Entities.Users.Contracts.Dtos;
using OAuth_Front.Exceptions;
using System.Net.Http.Json;

namespace OAuth_Front.Application.Entities.Users;

public class UserAppService : IUserService
{
    private readonly IHttpClientService _httpClientService;

    public UserAppService(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    public async Task<(List<GetAllUserDto> Users, CustomException Error)> GetAll()
    {
        try
        {
            var response = await _httpClientService.GetAsync("users/all");
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                return (null, CustomException.Failure($"{response.StatusCode}"));
            }


            var users = await response.Content.ReadFromJsonAsync<List<GetAllUserDto>>();
            return (users ?? new List<GetAllUserDto>(),CustomException.Success);
        }
        catch (HttpRequestException )
        {
            return (null, CustomException.Failure($"\"Failed to connect to the API. Please"));
        }
        catch(Exception)
        {
            return (null, CustomException.Failure("An unexpected error occured"));
        }

    }
}
