namespace PAR.Contracts.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public IEnumerable<string> Roles { get; set; } = null!;
}