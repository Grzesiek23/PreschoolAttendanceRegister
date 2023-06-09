using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Contracts.Dtos;

namespace PAR.Application.Features.SchoolYears.Queries;

public record GetSchoolYearByIdQuery : IRequest<SchoolYearDto?>
{
    public int Id { get; init; }
}

public class GetSchoolYearByIdHandler : IRequestHandler<GetSchoolYearByIdQuery, SchoolYearDto?>
{
    private readonly IParDbContext _dbContext;

    public GetSchoolYearByIdHandler(IParDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SchoolYearDto?> Handle(GetSchoolYearByIdQuery request, CancellationToken cancellationToken)
    {
       var entity = await _dbContext.SchoolYears.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
       
         if (entity == null) return null;

         return new SchoolYearDto
         {
             Id = entity.Id,
             StartDate = entity.StartDate,
             EndDate = entity.EndDate,
             IsCurrent = entity.IsCurrent
         };
    }
}