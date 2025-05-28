using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OAuth_Presentation.Pages.SignIn
{
    public class logOutModel : PageModel
    {

        public IActionResult OnGet()
        {
            // پاک کردن سشن یا کوکی
            HttpContext.Session.Remove("JwtToken");

            // اگر کوکی هم داشتی:
            // Response.Cookies.Delete("YourCookieName");

            // انتقال به صفحه لاگین یا صفحه اصلی
            return RedirectToPage("/SignIn/LogIn"); // یا هر صفحه‌ای که خواستی
        }

    }
}
