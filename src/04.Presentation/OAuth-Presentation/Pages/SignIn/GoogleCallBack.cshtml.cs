using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OAuth_Presentation.Models.Auth;
using OAuth_Presentation.Services.Authentication;
using System.Security.Claims;

namespace OAuth_Presentation.Pages.SignIn
{
    public class GoogleCallBackModel : PageModel
    {

        private readonly IAuthService _authService;

        public GoogleCallBackModel(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<IActionResult> OnGet()
        {
            var response = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!response.Succeeded || response.Principal == null)
            {
                return RedirectToPage("/SignIn/LogIn");
            }

            var email = response.Principal.FindFirst(ClaimTypes.Email)!.Value;
            var name = response.Principal.FindFirst(ClaimTypes.Name)!.Value;
            var frontUrl = $"{Request.Scheme}://{Request.Host}";


            var resultApi = await _authService.LogInWithGoogle(new LoginWithGoogleDto
            {
                Email = email,
                 FrontUri=frontUrl,
                 FullName=name
            });
            if (resultApi.IsSuccess)
            {
                HttpContext.Session.SetString("JwtToken", resultApi.Data.Token);
                return RedirectToPage("../Index");
            }
            return RedirectToPage("/SignIn/LogIn");


        }
    }
}
