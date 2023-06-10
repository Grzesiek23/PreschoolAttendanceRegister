using PAR.Contracts.Dtos;
using PAR.Contracts.Requests;
using PAR.Domain.Entities;

namespace PAR.Application.Mapping;

public static class GroupMapping
{
    public static GroupDto AsDto(this Group entity)
    {
        return new GroupDto
        {
            Id = entity.Id,
            Name = entity.Name,
            TeacherId = entity.TeacherId,
            SchoolYearId = entity.SchoolYearId,
            Teacher = entity.Teacher?.AsDtoWithoutNested(),
            SchoolYear = entity.SchoolYear?.AsDtoWithoutNested()
        };
    }
    
    public static GroupDto AsDtoWithoutNested(this Group entity)
    {
        return new GroupDto
        {
            Id = entity.Id,
            Name = entity.Name,
            TeacherId = entity.TeacherId,
            SchoolYearId = entity.SchoolYearId,
        };
    }

    public static Group AsEntity(this GroupRequest request)
    {
        return new Group
        {
            Id = request.Id,
            Name = request.Name,
            TeacherId = request.TeacherId,
            SchoolYearId = request.SchoolYearId
        };
    }
     
}