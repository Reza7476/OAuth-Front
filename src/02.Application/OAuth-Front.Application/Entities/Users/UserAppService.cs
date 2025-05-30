using OAuth_Front.Application.Entities.Users.Contracts;
using OAuth_Front.Application.Entities.Users.Contracts.Dtos;
using OAuth_Front.Builders;
using OAuth_Front.Dtos;
using System.Net.Http.Headers;
using System.Text.Json;

namespace OAuth_Front.Application.Entities.Users;

public class UserAppService 
{
    public UserAppService()
    {
    }

    public async Task<ApiResultDto<List<GetAllUserDto>>> GetAll(string token)
    {
       var client = new HttpClientBuilder()
           .WithToken(token)
           .Build();

        //var response = await client.GetAsync("https://oauth.rdehghai.ir/api/users/all");
        //var result = new ApiResultDto<List<GetAllUserDto>>();
        //var json = await response.Content.ReadAsStringAsync();

        //if (response.IsSuccessStatusCode)
        //{
        //    result.Success = true;
        //    try
        //    {
        //        result.Data = JsonSerializer.Deserialize<List<GetAllUserDto>>(json);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        //else
        //{
        //    result.Success = false;
        //}
        //return result;

        return null;
    }
}
