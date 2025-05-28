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
            roles = jwt.Claims
                .Where(c => c.Type == System.Security.Claims.ClaimTypes.Role
                         || c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
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
