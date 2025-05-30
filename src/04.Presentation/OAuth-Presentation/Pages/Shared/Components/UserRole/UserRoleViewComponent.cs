using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace OAuth_Presentation.Pages.Shared.Components.UserRole;

public class UserRoleViewComponent : ViewComponent
{

    public IViewComponentResult Invoke()
    {
        var token = HttpContext.Session.GetString("JwtToken");


        List<string> roles = new();

        if (!string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var claims = jwt.Claims;
            roles = jwt.Claims
                .Where(c => c.Type == "role")
                .Select(c => c.Value)
                .ToList();
        }
        return View(new UserRoleModel()
        {
            Roles = roles
        });
    }
}

public class UserRoleModel
{
    public List<string> Roles { get; set; } = new();
}
