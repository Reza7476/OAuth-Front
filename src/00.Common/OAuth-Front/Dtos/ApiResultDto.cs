namespace OAuth_Front.Dtos;

public class ApiResultDto<TData>
{
    public TData Data { get; set; } 
    public string? Error { get; set; }
    public string? Description { get; set; }
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; }
}
