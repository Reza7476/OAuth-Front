using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;

namespace OAuth_Presentation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public List<string>? UserRoles { get; set; }
        public string? UserFullName { get; set; }

        public void OnGet()
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var firstName = jwt.Claims.FirstOrDefault(c => c.Type == "FirstName")?.Value;

                // استخراج نام خانوادگی
                var lastName = jwt.Claims.FirstOrDefault(c => c.Type == "LastName")?.Value;

                var roles = jwt.Claims
                .Where(c => c.Type == System.Security.Claims.ClaimTypes.Role
                 || c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                  .Select(c => c.Value)
                  .ToList();

                UserRoles = new List<string>();
                foreach (var role in roles)
                {
                    UserRoles.Add(role);
                }

                UserFullName = $"{firstName} {lastName}";
            }
        }
    }
}
