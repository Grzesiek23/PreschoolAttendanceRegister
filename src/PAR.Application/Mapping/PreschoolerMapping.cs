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
            Group = entity.Group?.AsDtoWithoutNested(),
        };
    }

    public static PreschoolerDto AsDtoWithoutNested(this Preschooler entity)
    {
        return new PreschoolerDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            GroupId = entity.GroupId,
        };
    }
    
    public static PreschoolerDetailsDto AsDetailDto(this Preschooler entity)
    {
        return new PreschoolerDetailsDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            GroupId = entity.Group.Id,
            GroupName = entity.Group.Name,
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