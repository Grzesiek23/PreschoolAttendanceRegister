using MediatR;
using Microsoft.AspNetCore.Identity;
using PAR.Contracts.Dtos;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Roles.Queries;

public record GetRoleByIdQuery : IRequest<RoleDto?>
{
    public int Id { get; init; }
}

public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, RoleDto?>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    
    public GetRoleByIdHandler(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.Id.ToString());
        
        if (role == null)
            return null;

        var result = new RoleDto
        {
            Id = role.Id,
            Name = role.Name
        };
        
        return result;
    }
}