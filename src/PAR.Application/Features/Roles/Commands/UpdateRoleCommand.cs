using MediatR;
using Microsoft.AspNetCore.Identity;
using PAR.Application.Exceptions;
using PAR.Contracts.Requests;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Roles.Commands;

public record UpdateRoleCommand : IRequest<Unit>
{
    public int Id { get; init; }
    public RoleRequest RoleRequest { get; init; } = null!;
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
        if(request.Id != request.RoleRequest.Id)
            throw new BadRequestException(nameof(UpdateRoleCommand), $"Id in request body ({request.RoleRequest.Id}) does not match id in request path ({request.Id})");

        var role = await _roleManager.FindByIdAsync(request.RoleRequest.Id.ToString());
        if (role == null)
            throw new NotFoundException(nameof(UpdateRoleCommand), nameof(ApplicationRole), request.RoleRequest.Id!);

        role.Name = request.RoleRequest.Name;

        var result = await _roleManager.UpdateAsync(role);

        if (result.Succeeded) return Unit.Value;

        var errors = result.Errors.Select(x => x.Description);
        throw new InternalApplicationError(nameof(UpdateRoleCommand),
            $"Failed to update role: {string.Join(",", errors)}");
    }
}