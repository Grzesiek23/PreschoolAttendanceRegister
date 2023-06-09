using MediatR;
using Microsoft.AspNetCore.Identity;
using PAR.Application.Exceptions;
using PAR.Contracts.Requests;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Roles.Commands;

public record UpdateRoleCommand : IRequest<Unit>
{
    public UpdateRoleRequest UpdateRoleRequest { get; init; } = null!;
}

public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, Unit>
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public UpdateRoleHandler(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.UpdateRoleRequest.Id!);
        
        if (role == null) throw new NotFoundException(nameof(ApplicationRole), request.UpdateRoleRequest.Id!);
        
        role.Name = request.UpdateRoleRequest.Name;
        
        var result = await _roleManager.UpdateAsync(role);
        
        if (result.Succeeded) return Unit.Value;
        
        var errors = result.Errors.Select(x => x.Description);
        throw new InternalApplicationError(nameof(UpdateRoleCommand), $"Failed to update role: {string.Join(",", errors)}");
    }
}