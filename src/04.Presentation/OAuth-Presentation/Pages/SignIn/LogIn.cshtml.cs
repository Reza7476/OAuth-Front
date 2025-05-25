using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OAuth_Front.Application.Register.Contracts;
using OAuth_Front.Application.Register.Contracts.Dtos;

namespace OAuth_Presentation.Pages.SignIn
{

    public class LogInModel : PageModel
    {
        private readonly IRegisterService _registerService;

        public LogInModel(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [BindProperty]
        public LogInDto Dto { get; set; }

        public string Result { get; set; }
        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPost()
        {
            Result = await _registerService.LogIn(Dto);
            return RedirectToPage("/index");
        }
    }
}
