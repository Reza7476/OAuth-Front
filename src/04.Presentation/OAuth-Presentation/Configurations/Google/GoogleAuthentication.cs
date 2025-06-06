using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace OAuth_Presentation.Configurations.Google;

public static class GoogleAuthentication
{
    public static IServiceCollection AddGoogleAuth(this IServiceCollection service,IConfiguration configuration)
    {
        service.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
            .AddCookie()
            .AddGoogle(option =>
            {

                option.ClientId = configuration["Google:ClientId"]!;
                option.ClientSecret = configuration["Google:SecretId"]!;
            });
        return service;
    } 
}
