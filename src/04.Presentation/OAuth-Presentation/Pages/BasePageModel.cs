using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace OAuth_Presentation.Pages;

public class BasePageModel : PageModel
{
    public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
        var handler = new JwtSecurityTokenHandler();

        var token = HttpContext.Session.GetString("JwtToken");


        if (string.IsNullOrEmpty(token))
        {
            context.Result = RedirectToPage("/SignIn/LogIn");
        }
        else
        {
            var jwt = handler.ReadJwtToken(token);
            var exp = jwt.ValidTo;
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer" ,token);
            if (exp < DateTime.UtcNow)
            {
                context.Result = RedirectToPage("/SignIn/LogIn");
            }
        }


        base.OnPageHandlerExecuted(context);
    }

}
