using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PAR.Contracts.Dtos;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Roles.Queries;

public record GetAllRolesQuery : IRequest<IEnumerable<RoleDto>>;

public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleDto>>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    
    public GetAllRolesHandler(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IEnumerable<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.Roles.ToListAsync(cancellationToken);

        var result = role.Select(x => new RoleDto
        {
            Id = x.Id,
            Name = x.Name
        });
        
        return result;
    }
}