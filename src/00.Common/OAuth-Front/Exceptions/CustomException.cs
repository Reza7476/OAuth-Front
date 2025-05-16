namespace OAuth_Front.Exceptions;

public class CustomException : Exception
{
    public bool HasError { get; set; }
    public string? Message { get; set; }

    public static CustomException Success => new CustomException { HasError = false, Message = string.Empty };
    public static CustomException Failure(string message) => new CustomException { HasError = true, Message = message };
}
