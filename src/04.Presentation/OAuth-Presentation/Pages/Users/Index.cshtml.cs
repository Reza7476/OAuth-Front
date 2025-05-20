using Microsoft.AspNetCore.Mvc.RazorPages;
using OAuth_Front.Application.Entities.Users.Contracts;
using OAuth_Front.Application.Entities.Users.Contracts.Dtos;
using System.Reflection.Metadata;

namespace OAuth_Presentation.Pages.Users;

public class IndexModel : PageModel
{
    private readonly IUserService _service;

    public IndexModel(IUserService service)
    {
        _service = service;
    }

    public List<GetAllUserDto> GetAllUsers { get; set; }

    public async Task OnGet(string referrer)
    {
        var users = await _service.GetAll();
        GetAllUsers = users;
    }
}
