using PAR.Contracts.Dtos;
using PAR.Contracts.Requests;
using PAR.Domain.Entities;

namespace PAR.Application.Mapping;

public static class SchoolYearMapping
{
    public static SchoolYearDto AsDto(this SchoolYear apiClient)
    {
        return new SchoolYearDto
        {
            Id = apiClient.Id,
            StartDate = apiClient.StartDate,
            EndDate = apiClient.EndDate,
            IsCurrent = apiClient.IsCurrent
        };
    }

    public static SchoolYear AsEntity(this SchoolYearRequest request)
    {
        return new SchoolYear
        {
            Id = request.Id,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsCurrent = request.IsCurrent
        };
    }
}