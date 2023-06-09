using MediatR;
using Microsoft.AspNetCore.Identity;
using PAR.Application.Exceptions;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Users.Commands;

public record RemoveUserFromRoleCommand : IRequest<Unit>
{
    public string? UserId { get; init; }
    public string? RoleId { get; init; }
}

public class RemoveUserFromRoleHandler : IRequestHandler<RemoveUserFromRoleCommand, Unit>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public RemoveUserFromRoleHandler(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(RemoveUserFromRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            throw new NotFoundException(nameof(RemoveUserFromRoleCommand), nameof(ApplicationUser), request.UserId);

        var role = await _roleManager.FindByIdAsync(request.RoleId);
        if (role == null)
            throw new NotFoundException(nameof(RemoveUserFromRoleCommand), nameof(ApplicationRole), request.RoleId);

        var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
        if (!isInRole)
            throw new BadRequestException(nameof(RemoveUserFromRoleCommand),$"User {user.UserName} is not in role {role.Name}");
        
        var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
        if (!result.Succeeded)
            throw new InternalApplicationError(nameof(RemoveUserFromRoleCommand), $"Could not remove user {user.UserName} from role {role.Name}");

        return Unit.Value;
    }
}