using OAuth_Front.Application.Register.Contracts.Dtos;
using OAuth_Front.Dtos;
using OAuth_Presentation.Models.Auth;

namespace OAuth_Presentation.Services.Authentication;

public class AuthenticationService : IAuthService
{
    private readonly HttpClient _client;

    public AuthenticationService(HttpClient client, string baseAddress)
    {
        _client = client;
        _client.BaseAddress = new Uri(baseAddress);
    }

    public async Task<ApiResultDto<LoginResponse>> LogIn(LogInDto dto)
    {
        var result = new ApiResultDto<LoginResponse>();
        var response = await _client.PostAsJsonAsync("SignIn/Login", dto);
        var resultContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            result.StatusCode = 200;
            result.Data = new LoginResponse
            {
                Token = resultContent
            };
            result.IsSuccess = response.IsSuccessStatusCode;
        }
        else
        {
            using var doc = System.Text.Json.JsonDocument.Parse(resultContent);
            var root = doc.RootElement;

            if (root.TryGetProperty("error", out var errorProp))
                result.Error = errorProp.GetString();

            if (root.TryGetProperty("description", out var descProp))
                result.Description = descProp.GetString();

            result.StatusCode = (int)response.StatusCode;
            result.IsSuccess = response.IsSuccessStatusCode;
        }

        return result;
    }
}
