namespace OAuth_Presentation.Models.Commons;

public class AlertMessage
{
    public string Title { get; set; }
    public string Message { get; set; }
    public AlertType Type { get; set; } // Success, Error, Info, Warning
    public bool IsPersistent { get; set; } = false; // اگر می‌خواهی پیام بعد از reload هم بماند
}
public enum AlertType
{
    Success,
    Error,
    Info,
    Warning
}