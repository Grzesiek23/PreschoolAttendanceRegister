namespace PAR.Application.Configuration;

public class JwtSettings
{
    public string? Key { get; init; }
    public string? Issuer { get; init; }
    public string? Audience { get; init; }
    public int TokenLifetimeHours { get; init; }
}