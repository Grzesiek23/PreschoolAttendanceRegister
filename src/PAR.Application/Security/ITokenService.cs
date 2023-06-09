using PAR.Contracts.Dtos;
using PAR.Domain.Entities;

namespace PAR.Application.Security;

public interface ITokenService
{
    AuthDto CreateToken(ApplicationUser user, IEnumerable<string> roles);
}