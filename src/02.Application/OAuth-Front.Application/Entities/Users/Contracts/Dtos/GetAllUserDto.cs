namespace OAuth_Front.Application.Entities.Users.Contracts.Dtos;

public class GetAllUserDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string UserName { get; set; }
    public required string LastName { get; set; }
    public required string Mobile { get; set; }
}
