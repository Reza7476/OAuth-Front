using Microsoft.AspNetCore.Mvc.RazorPages;
using OAuth_Front.Application.Entities.Users.Contracts;

namespace OAuth_Presentation.Pages.Users;

public class IndexModel : PageModel
{
    private readonly IUserService _service;
    public IndexModel(IUserService service)
    {
        _service = service;
    }

    public void OnGet()
    {

    }
}
