using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OAuth_Presentation.Models.Auth;
using OAuth_Presentation.Services.Authentication;
using System.ComponentModel.DataAnnotations;

namespace OAuth_Presentation.Pages.SignIn
{
    [BindProperties]
    public class LogInModel : PageModel
    {
        [Required(ErrorMessage = "نام کاربری را وارد کنید")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        public string Password { get; set; }

        private readonly IAuthService _authService;
        public LogInModel(IAuthService authService)
        {
            _authService = authService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _authService.LogIn(new LogInDto()
            {
                Password = Password,
                UserName = UserName,
                SiteAudience = "oauth.front.rdehghai.ir"
            });

            if (result.IsSuccess)
            {
                HttpContext.Session.SetString("JwtToken", result.Data.Token);
                return RedirectToPage("../Index");
            }
            ModelState.AddModelError(nameof(UserName), result.Error);
            return Page();

        }
    }
}
