using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OAuth_Front.Application.Entities.Users.Contracts;
using OAuth_Front.Application.Entities.Users.Contracts.Dtos;
using OAuth_Front.Dtos;

namespace OAuth_Presentation.Pages.Users;

public class IndexModel : PageModel
{
    private readonly IUserService _service;



    public IndexModel(IUserService service)
    {
        _service = service;
    }

    public ApiResultDto<List<GetAllUserDto>> GetAllUsers { get; set; } = new();


    public async Task OnGet()
    {
        var token = HttpContext.Session.GetString("JwtToken");
        var a = $"{HttpContext.Request.Scheme}://";
        var b = $"{HttpContext.Request.Host}";
        var c = $"{HttpContext.Request.Path}";
        var d = $"{HttpContext.Request.QueryString}";
        var dd = a + b;
        var fullUrl = a + b + c + d;
        Redirect(a + b);
        var users = await _service.GetAll(token!);
        if (users.Success)
        {
            GetAllUsers = users;
        }
        else
        {
            HttpContext.Response.Cookies.Append("ErrorTitle", users.StatusCode.ToString());
            HttpContext.Response.Cookies.Append("ErrorTitle", users.Error!);

            Redirect(a + b);
        }

    }
}
