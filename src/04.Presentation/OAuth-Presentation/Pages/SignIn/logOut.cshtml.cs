using Microsoft.AspNetCore.Mvc;

namespace OAuth_Presentation.Pages.SignIn
{
    public class logOutModel : BasePageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Remove("JwtToken");
            return RedirectToPage("/SignIn/LogIn"); 
        }
    }
}
