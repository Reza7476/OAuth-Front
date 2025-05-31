using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OAuth_Presentation.Models.Commons;
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
    protected void ShowAlert(string message, string title = null, AlertType type = AlertType.Info, bool persistent = false)
    {
        var alert = new AlertMessage
        {
            Title = title,
            Message = message,
            Type = type,
            IsPersistent = persistent
        };
        TempData["AlertMessage"] = System.Text.Json.JsonSerializer.Serialize(alert);
    }

    protected void ShowError(string message, string title = "خطا", bool persistent = false)
        => ShowAlert(message, title, AlertType.Error, persistent);

    protected void ShowSuccess(string message, string title = "موفقیت", bool persistent = false)
        => ShowAlert(message, title, AlertType.Success, persistent);

    protected void ShowWarning(string message, string title = "هشدار", bool persistent = false)
        => ShowAlert(message, title, AlertType.Warning, persistent);

    protected void ShowInfo(string message, string title = "اطلاع", bool persistent = false)
        => ShowAlert(message, title, AlertType.Info, persistent);

    // برای جمع‌آوری خطاهای ModelState
    protected string GetModelStateErrors()
    {
        var errors = ModelState.Values.SelectMany(v => v.Errors)
                         .Select(e => !string.IsNullOrEmpty(e.ErrorMessage) ? e.ErrorMessage : e.Exception?.Message)
                         .ToList();
        return string.Join("<br/>", errors);
    }
}
