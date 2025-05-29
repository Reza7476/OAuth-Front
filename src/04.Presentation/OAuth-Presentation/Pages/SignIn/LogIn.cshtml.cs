using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OAuth_Front.Application.Register.Contracts;
using OAuth_Front.Application.Register.Contracts.Dtos;
using OAuth_Front.Dtos;

namespace OAuth_Presentation.Pages.SignIn
{

    public class LogInModel : PageModel
    {
        private readonly IRegisterService _registerService;

        public LogInModel(
            IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [BindProperty]
        public LogInDto Dto { get; set; } = default!;

        public ApiResultDto<string> Result { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            Result = await _registerService.LogIn(Dto);
            if (Result.Success)
            {
                HttpContext.Session.SetString("JwtToken", Result.Data!);
                return RedirectToPage("../Index");
            }
            else
            {
                HttpContext.Response.Cookies.Append("ErrorTitle", Result.StatusCode.ToString());
                HttpContext.Response.Cookies.Append("ErrorMessage", Result.Error!);
                return Page();
            }
        }
    }
}
