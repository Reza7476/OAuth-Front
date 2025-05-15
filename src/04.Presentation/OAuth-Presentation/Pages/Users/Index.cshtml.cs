using Microsoft.AspNetCore.Mvc.RazorPages;
using OAuth_Front.Application.Entities.Users.Contracts;

namespace OAuth_Presentation.Pages.Users;

public class IndexModel : PageModel
{

    public string Id { get; set; }
    public string  Name { get; set; }
    public string  LastName { get; set; }
    public string  UserName { get; set; }



    private readonly IUserService _service;
    public IndexModel(IUserService service)
    {
        _service = service;
    }

    public async Task<List<IndexModel>> OnGet()
    {
        var users = await _service.GetAll();
    }
}
