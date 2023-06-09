using MediatR;
using Microsoft.AspNetCore.Identity;
using PAR.Application.Exceptions;
using PAR.Application.Security;
using PAR.Contracts.Dtos;
using PAR.Contracts.Requests;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Authorization.Commands;

public record LoginCommand : IRequest<AuthDto>
{
    public LoginRequest LoginRequest { get; init; }
}

public class LoginHandler : IRequestHandler<LoginCommand, AuthDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;

    public LoginHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<AuthDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.LoginRequest.Email);

        if (user == null)
            throw new NotFoundException(nameof(LoginCommand), nameof(ApplicationUser), request.LoginRequest.Email);
        
        var checkPasswordAsync = await _userManager.CheckPasswordAsync(user, request.LoginRequest.Password);
        
        if (!checkPasswordAsync) 
            throw new InvalidCredentialsException();
        
        var roles = await _userManager.GetRolesAsync(user);
        
        var result = _tokenService.CreateToken(user, roles);

        return result;
    }
}