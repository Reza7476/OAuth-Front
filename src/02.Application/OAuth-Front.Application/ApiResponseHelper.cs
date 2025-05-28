using OAuth_Front.Exceptions;
using System.Text.Json;

namespace OAuth_Front.Application;

public static class ApiResponseHelper
{
    public static async Task<T> HandleApiResponse<T>(HttpResponseMessage response)
    {
        var stringContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            // اگر خطا بود،
            // خطا را به مدل مشترک دسرسیالایز کن و
            // Exception
            // بنداز
            var error = JsonSerializer
                .Deserialize<ApiErrorDto>(stringContent);
            throw new ApiException()
            {
                Error = error!.Error,
                Description = error.Description,
                StatusCode = error.StatusCode,
            };
        }

        return JsonSerializer.Deserialize<T>(stringContent)!;
    }
}
