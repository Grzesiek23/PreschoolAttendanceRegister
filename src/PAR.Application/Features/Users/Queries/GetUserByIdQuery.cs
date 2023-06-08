using MediatR;
using Microsoft.AspNetCore.Identity;
using PAR.Contracts.Dtos;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Users.Queries;

public record GetUserByIdQuery : IRequest<UserDto?>
{
    public string Id { get; init; } = null!;
}

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetUserByIdHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);

        if (user == null)
            return null;

        var roles = await _userManager.GetRolesAsync(user);

        var result = new UserDto
        {
            Id = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Roles = roles
        };

        return result;
    }
}