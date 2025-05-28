using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IdentityModel.Tokens.Jwt;

namespace OAuth_Presentation.Pages.Shared.Components.UserInfo;

public class UserInfoViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var token = HttpContext.Session.GetString("JwtToken");
        string? fullName = null;

        if (!string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var firstName = jwt.Claims.FirstOrDefault(_ => _.Type == "FirstName")?.Value ?? "";
            var lastName = jwt.Claims.FirstOrDefault(_ => _.Type == "LastName")?.Value ?? "";
            fullName = string.Join(" ", new[] { firstName, lastName }.Where(x => !string.IsNullOrWhiteSpace(x)));
        }
        return View(new UserInfoModel()
        {
            FullName = fullName,
        });
    }
}
public class UserInfoModel
{
    public string? FullName { get; set; }
}
