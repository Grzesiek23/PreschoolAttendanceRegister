using PAR.Contracts.Dtos;
using PAR.Domain.Entities;

namespace PAR.Application.Mapping;

public static class UserMapping
{
    public static UserDto AsDto(this ApplicationUser entity)
    {
        return new UserDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
        };
    }
    
    public static UserDto AsDtoWithoutNested(this ApplicationUser entity)
    {
        return new UserDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
        };
    }
}