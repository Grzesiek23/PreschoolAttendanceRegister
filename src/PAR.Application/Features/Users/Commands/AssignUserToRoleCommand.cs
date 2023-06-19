using MediatR;
using Microsoft.AspNetCore.Identity;
using PAR.Application.Exceptions;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Users.Commands;

public record AssignUserToRoleCommand : IRequest<Unit>
{
    public int? UserId { get; init; }
    public int? RoleId { get; init; }
}

public class AssignUserToRoleHandler : IRequestHandler<AssignUserToRoleCommand, Unit>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AssignUserToRoleHandler(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(AssignUserToRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            throw new NotFoundException(nameof(AssignUserToRoleCommand), nameof(ApplicationUser), request.UserId);
        
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role == null)
            throw new NotFoundException(nameof(AssignUserToRoleCommand), nameof(ApplicationRole), request.RoleId);

        await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
        
        var result = await _userManager.AddToRoleAsync(user!, role!.Name!);

        if (result.Succeeded) return Unit.Value;

        var errors = result.Errors.Select(x => x.Description);
        throw new InternalApplicationError(nameof(AssignUserToRoleCommand),
            $"Failed to assign user {request.UserId} to role: {request.RoleId}, with errors: {string.Join(",", errors)}");
    }
}