using PAR.Contracts.Dtos;
using PAR.Contracts.Requests;
using PAR.Domain.Entities;

namespace PAR.Application.Mapping;

public static class PreschoolerMapping
{
    public static PreschoolerDto AsDto(this Preschooler entity)
    {
        return new PreschoolerDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            GroupId = entity.GroupId,
        };
    }

    public static Preschooler AsEntity(this PreschoolerRequest request)
    {
        return new Preschooler
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            GroupId = request.GroupId,
        };
    }
     
}