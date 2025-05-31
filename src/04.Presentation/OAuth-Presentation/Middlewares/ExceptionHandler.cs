using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;

namespace OAuth_Presentation.Middlewares;

public static  class ExceptionHandler
{

    public static IApplicationBuilder UseFrontExceptionHandler(this IApplicationBuilder app)
    {
        var environment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
        var jsonOption = app.ApplicationServices.GetService<IOptions<JsonOptions>>()?.Value.SerializerOptions;

        app.UseExceptionHandler(_ => _.Run(async context =>
        {
            var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
            var result = new ExceptionErrorDto();
            result.StatusCode=context.Response.StatusCode;
            result.Description = exception?.ToString();

        }));

        return app;
    }
}

public class ExceptionErrorDto
{
    public string? Error { get; set; }
    public string? Description { get; set; }
    public int StatusCode { get; set; }
}
