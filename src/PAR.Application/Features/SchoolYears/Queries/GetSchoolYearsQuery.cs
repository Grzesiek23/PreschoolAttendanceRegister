using MediatR;
using PAR.Application.DataAccessLayer;
using PAR.Application.Extensions;
using PAR.Application.Mapping;
using PAR.Contracts;
using PAR.Contracts.Dtos;
using PAR.Contracts.Requests.Options;

namespace PAR.Application.Features.SchoolYears.Queries;

public record GetSchoolYearsQuery : IRequest<PagedResponse<SchoolYearDto>>
{
    public GetSchoolYearsOptionsRequest GetSchoolYearsOptionsRequest { get; init; } = null!;
}

public class GetSchoolYearsHandler : IRequestHandler<GetSchoolYearsQuery, PagedResponse<SchoolYearDto>>
{
    private readonly IParDbContext _dbContext;

    public GetSchoolYearsHandler(IParDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<SchoolYearDto>> Handle(GetSchoolYearsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.SchoolYears.Where(x => x.IsActive).AsQueryable();
        
        if (request.GetSchoolYearsOptionsRequest.StartDate.HasValue)
            query = query.Where(x => x.StartDate >= request.GetSchoolYearsOptionsRequest.StartDate);
        
        if (request.GetSchoolYearsOptionsRequest.EndDate.HasValue)
            query = query.Where(x => x.EndDate <= request.GetSchoolYearsOptionsRequest.EndDate);

        if(request.GetSchoolYearsOptionsRequest.IsCurrent.HasValue)
            query = query.Where(x => x.IsCurrent == request.GetSchoolYearsOptionsRequest.IsCurrent);
        
        var result = await query.ToPagedResponseAsync(request.GetSchoolYearsOptionsRequest, entity => entity.AsDto(),
            cancellationToken);
        
        return result;
    }
}