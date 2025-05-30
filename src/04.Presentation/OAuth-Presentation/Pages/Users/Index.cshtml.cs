using Microsoft.AspNetCore.Mvc;
using OAuth_Front.Application.Entities.Users.Contracts.Dtos;
using OAuth_Front.Dtos;
using OAuth_Presentation.Services.Users;
using System.Reflection.Metadata.Ecma335;

namespace OAuth_Presentation.Pages.Users;

public class IndexModel : BasePageModel
{

    private readonly IUserService _service;
    public IndexModel(IUserService service)
    {
        _service = service;
    }


    public async Task<IActionResult> OnGet()
    {
        var token = HttpContext.Session.GetString("JwtToken");
        var users = await _service.GetAll(token);

        return new JsonResult(new {data=users.Data,success=users.IsSuccess});
    }

}