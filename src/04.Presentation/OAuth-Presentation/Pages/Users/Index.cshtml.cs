using Microsoft.AspNetCore.Mvc.RazorPages;
using OAuth_Front.Application.Entities.Users.Contracts;
using OAuth_Front.Application.Entities.Users.Contracts.Dtos;

namespace OAuth_Presentation.Pages.Users;

public class IndexModel : PageModel
{
    private readonly IUserService _service;

    public IndexModel(IUserService service)
    {
        _service = service;
    }

    public List<GetAllUserDto> GetAllUsers { get; set; }

    public async Task OnGet()
    {

        var (users, error) = await _service.GetAll();
        if (error.HasError)
        {
            TempData["Error"] = error.Message;
            GetAllUsers=new List<GetAllUserDto>();
            return;
        }

        GetAllUsers = users;



    }
}
