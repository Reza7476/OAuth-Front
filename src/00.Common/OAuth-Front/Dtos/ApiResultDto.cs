namespace OAuth_Front.Dtos;

public class ApiResultDto<T>
{
    public T? Data { get; set; } = default(T?);
    public string? Error { get; set; }
    public string? Description { get; set; }
    public int StatusCode { get; set; }
    public bool Success { get; set; }
}
