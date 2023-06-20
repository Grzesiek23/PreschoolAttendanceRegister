using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Contracts.Dtos;

namespace PAR.Application.Features.SchoolYears.Queries;

public record GetSchoolYearsAsOptionListQuery : IRequest<IEnumerable<NumberListDto>>;

public class GetSchoolYearsAsOptionListHandler : IRequestHandler<GetSchoolYearsAsOptionListQuery, IEnumerable<NumberListDto>>
{
    private readonly IParDbContext _dbContext;

    public GetSchoolYearsAsOptionListHandler(IParDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<NumberListDto>> Handle(GetSchoolYearsAsOptionListQuery request,
        CancellationToken cancellationToken)
    {
        var query = await _dbContext.SchoolYears.Where(x => x.IsActive).ToListAsync(cancellationToken);
        
       var result = query.Select(x => new NumberListDto
        {
            Id = x.Id,
            Name = x.Name
        });
        
        return result;
    }
}