using PAR.Contracts.Dtos;
using PAR.Domain.Entities;

namespace PAR.Application.Mapping;

public static class RoleMapping
{
    public static RoleDto AsDto(this ApplicationRole entity)
    {
        return new RoleDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}