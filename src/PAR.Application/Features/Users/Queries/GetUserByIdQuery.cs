using MediatR;
using Microsoft.AspNetCore.Identity;
using PAR.Application.Mapping;
using PAR.Contracts.Dtos;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Users.Queries;

public record GetUserByIdQuery : IRequest<UserDto?>
{
    public int Id { get; init; }
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
        var user = await _userManager.FindByIdAsync(request.Id.ToString());

        if (user == null)
            return null;

        var roles = await _userManager.GetRolesAsync(user);

        var result = user.AsDto();
        result.Roles = roles;

        return result;
    }
}