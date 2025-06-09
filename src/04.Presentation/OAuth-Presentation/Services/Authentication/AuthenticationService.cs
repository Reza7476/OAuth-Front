using OAuth_Presentation.Models.Auth;
using OAuth_Presentation.Models.Commons;

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
        var response = await _client.PostAsJsonAsync("SignIn/Login", dto);
        var resultContent = await response.Content.ReadAsStringAsync();

        var result = new ApiResultDto<LoginResponse>();
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

    public async Task<ApiResultDto<LoginResponse>> LogInWithGoogle(LoginWithGoogleDto dto)
    {
        var response = await _client.PostAsJsonAsync("SignIn/logIn-with-google", dto);
        var resultContent = await response.Content.ReadAsStringAsync();

        var result = new ApiResultDto<LoginResponse>();
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
            if (resultContent == null)
            {
               result.StatusCode = (int)response.StatusCode;
               result.IsSuccess = response.IsSuccessStatusCode;
                result.Error = "error ";
                return result;

            }
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
