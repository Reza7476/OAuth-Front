using System.ComponentModel.DataAnnotations;

namespace OAuth_Front.Application.Register.Contracts.Dtos;

public class LogInDto
{
    [Required]
    public required string UserName { get; set; }

    [Required]
    public required string Password { get; set; }

    [Required]
    public required string SiteAudience { get; set; }
}
