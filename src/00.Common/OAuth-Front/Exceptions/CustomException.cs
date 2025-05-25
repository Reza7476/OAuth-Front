namespace OAuth_Front.Exceptions;

public class CustomException : Exception
{
    public bool HasError { get; set; }
    public string? ErrorMessage { get; set; }

    public static CustomException Success => new CustomException { HasError = false, ErrorMessage = string.Empty };
    public static CustomException Failure(string message) => new CustomException { HasError = true, ErrorMessage = message };
}
