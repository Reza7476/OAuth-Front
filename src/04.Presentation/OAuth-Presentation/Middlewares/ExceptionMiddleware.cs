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
            // تعیین عنوان مناسب بر اساس کد خطا
            string title = statusCode switch
            {
                HttpStatusCode.BadRequest => "درخواست نامعتبر!",
                HttpStatusCode.Unauthorized => "دسترسی غیرمجاز!",
                HttpStatusCode.NotFound => "یافت نشد!",
                _ when ((int)statusCode >= 500 && (int)statusCode < 600) => "خطای سرور!",
                _ => "خطا!"
            };
            string message = ex.Message;

            string referrer = context.Request.Query["referrer"]!;
            if (string.IsNullOrWhiteSpace(referrer))
                referrer = "/";

            // ذخیره title و message در دو کوکی جداگانه
            context.Response.Cookies.Append("ErrorTitle", title, new CookieOptions { Path = "/", Expires = DateTimeOffset.Now.AddMinutes(1) });
            context.Response.Cookies.Append("ErrorMessage", message, new CookieOptions { Path = "/", Expires = DateTimeOffset.Now.AddMinutes(1) });

            context.Response.Redirect(referrer);
            return Task.CompletedTask;
        }


        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            string message = ex.Message;
            string referrer = context.Request.Query["referrer"]!;
            if (string.IsNullOrWhiteSpace(referrer))
                referrer = "/";
            // خطاهای عمومی (مثل Exceptionهای داخلی)
            context.Response.Cookies.Append(
                "ErrorTitle",
                "server error",
                new CookieOptions
                {
                    Path = "/",
                    Expires = DateTimeOffset.Now.AddMinutes(1)
                });
            context.Response.Cookies.Append("ErrorMessage", message, new CookieOptions { Path = "/", Expires = DateTimeOffset.Now.AddMinutes(1) });
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