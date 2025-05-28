namespace OAuth_Front.Exceptions;

public class ApiException : Exception
{
    public string? Error { get; set; }
    public string? Description { get; set; }
    public int StatusCode { get; set; }

}
