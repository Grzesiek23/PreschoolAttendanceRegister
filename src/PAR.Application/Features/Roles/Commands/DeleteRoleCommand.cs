using MediatR;
using Microsoft.AspNetCore.Identity;
using PAR.Application.Exceptions;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Roles.Commands;

public record DeleteRoleCommand : IRequest<Unit>
{
    public string? Id { get; init; }
}

public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, Unit>
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public DeleteRoleHandler(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.Id!);
        
        if (role == null)
            throw new NotFoundException(nameof(ApplicationRole), request.Id!);
        
        var result = await _roleManager.DeleteAsync(role);

        if (result.Succeeded) return Unit.Value;
        
        var errors = result.Errors.Select(x => x.Description);
        throw new InternalApplicationError(nameof(DeleteRoleCommand),
            $"Failed to delete role: {string.Join(",", errors)}");
    }
}