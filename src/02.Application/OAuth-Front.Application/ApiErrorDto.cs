namespace OAuth_Front.Application;

public class ApiErrorDto
{
    public string? Error { get; set; }
    public string? Description { get; set; }
    public int StatusCode { get; set; }
}
