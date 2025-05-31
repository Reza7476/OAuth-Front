using Microsoft.AspNetCore.Mvc;
using OAuth_Presentation.Services.Users;

namespace OAuth_Presentation.Pages.Users;

public class IndexModel : BasePageModel
{

    private readonly IUserService _service;
    public IndexModel(IUserService service)
    {
        _service = service;
    }


    public void OnGet()
    {

    }

    public async Task<IActionResult> OnGetUsersAsync()
    {
        var token = HttpContext.Session.GetString("JwtToken");
        var users = await _service.GetAll(token);
        return new JsonResult(new
        {
            data = users.Data,
            success =users.IsSuccess,
            statusCode=users.StatusCode,
            error=users.Error,
        });
    }
}