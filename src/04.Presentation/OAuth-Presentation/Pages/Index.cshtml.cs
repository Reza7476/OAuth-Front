﻿namespace OAuth_Presentation.Pages;

public class IndexModel : BasePageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }


    public void OnGet()
    {
        HttpContext.Session.GetString("JwtToken");
    }
}
