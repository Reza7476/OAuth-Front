using System.Net;

namespace OAuth_Presentation.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpRequestException ex) when (ex.StatusCode.HasValue)
            {
                await HandleHttpRequestExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleHttpRequestExceptionAsync(HttpContext context, HttpRequestException ex)
        {
            var statusCode = ex.StatusCode ?? HttpStatusCode.InternalServerError;
            string redirectUrl = statusCode switch
            {
                HttpStatusCode.BadRequest => "/Errors/400", // خطای 400
                HttpStatusCode.Unauthorized => "/Errors/400", // خطای 401
                HttpStatusCode.NotFound => "/Errors/400", // خطای 404
                _ when ((int)statusCode >= 500 && (int)statusCode < 600) => "/Errors/500", // خطاهای 500-599
                _ => "/Error"
            };

            context.Response.Redirect(redirectUrl);
            return Task.CompletedTask;
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // خطاهای عمومی (مثل Exceptionهای داخلی)
            context.Response.Redirect("/Error/500");
            return Task.CompletedTask;
        }
    }

    // Extension method برای اضافه کردن Middleware
    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}