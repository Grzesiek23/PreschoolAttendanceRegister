namespace PAR.Contracts.Dtos;

public class AuthDto
{
    public int Id { get; init; }
    public string Email { get; init; } = null!;
    public string Token { get; init; } = null!;
}