namespace OAuth_Presentation.Models.Auth;

public class LoginWithGoogleDto
{
    public required string Email { get; set; }
    public required string FullName { get; set; }
    public required string FrontUri { get; set; }
}
