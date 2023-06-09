using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PAR.Application.Abstractions;
using PAR.Application.Configuration;
using PAR.Application.Security;
using PAR.Contracts.Dtos;
using PAR.Domain.Entities;

namespace PAR.Infrastructure.Authorization;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IClock _clock;

    public TokenService(IOptions<JwtSettings> jwtSettings, IClock clock)
    {
        _clock = clock;
        _jwtSettings = jwtSettings.Value;
    }

    public AuthDto CreateToken(ApplicationUser user, IEnumerable<string> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, user.Email),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new("fullName", string.IsNullOrWhiteSpace(user.FullName) ? user.Email : user.FullName),
            new("userId", user.Id),
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = _clock.Current().Add(TimeSpan.FromHours(_jwtSettings.TokenLifetimeHours)),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var jwt = tokenHandler.WriteToken(token);

        return new AuthDto {Id = user.Id, Email = user.Email, Token = jwt};
    }
}