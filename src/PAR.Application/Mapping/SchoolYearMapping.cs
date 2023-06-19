using PAR.Contracts.Dtos;
using PAR.Contracts.Requests;
using PAR.Domain.Entities;

namespace PAR.Application.Mapping;

public static class SchoolYearMapping
{
    public static SchoolYearDto AsDto(this SchoolYear entity)
    {
        return new SchoolYearDto
        {
            Id = entity.Id,
            Name = entity.Name,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            IsCurrent = entity.IsCurrent
        };
    }

    public static SchoolYearDto AsDtoWithoutNested(this SchoolYear entity)
    {
        return new SchoolYearDto
        {
            Id = entity.Id,
            Name = entity.Name,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            IsCurrent = entity.IsCurrent
        };
    }
    
    public static SchoolYear AsEntity(this SchoolYearRequest request)
    {
        return new SchoolYear
        {
            Id = request.Id,
            Name = request.Name,
            StartDate = DateOnly.FromDateTime(request.StartDate),
            EndDate = DateOnly.FromDateTime(request.EndDate),
            IsCurrent = request.IsCurrent
        };
    }
}