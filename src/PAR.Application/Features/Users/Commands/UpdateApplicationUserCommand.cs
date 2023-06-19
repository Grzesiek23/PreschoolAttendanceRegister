using MediatR;
using Microsoft.AspNetCore.Identity;
using PAR.Application.Exceptions;
using PAR.Contracts.Requests;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Users.Commands;

public record UpdateApplicationUserCommand : IRequest<Unit>
{
    public int Id { get; init; }
    public ApplicationUserUpdateRequest ApplicationUserUpdateRequest { get; init; } = null!;
}

public class UpdateRoleHandler : IRequestHandler<UpdateApplicationUserCommand, Unit>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdateRoleHandler(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(UpdateApplicationUserCommand request, CancellationToken cancellationToken)
    {
        if(request.Id != request.ApplicationUserUpdateRequest.Id)
            throw new BadRequestException(nameof(UpdateApplicationUserCommand), $"Id in request body ({request.ApplicationUserUpdateRequest.Id}) does not match id in request path ({request.Id})");

        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user == null)
            throw new NotFoundException(nameof(UpdateApplicationUserCommand), nameof(ApplicationUser), request.Id);
        
        user.FirstName = request.ApplicationUserUpdateRequest.FirstName;
        user.LastName = request.ApplicationUserUpdateRequest.LastName;
        user.PhoneNumber = request.ApplicationUserUpdateRequest.PhoneNumber;
        
        await _userManager.UpdateAsync(user);
        
        var role = await _roleManager.FindByIdAsync(request.ApplicationUserUpdateRequest.RoleId.ToString());
        if (role == null)
            throw new NotFoundException(nameof(UpdateApplicationUserCommand), nameof(ApplicationRole), request.ApplicationUserUpdateRequest.RoleId!);
        
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);

        await _userManager.AddToRoleAsync(user, role.Name!);

        return Unit.Value;
    }
}