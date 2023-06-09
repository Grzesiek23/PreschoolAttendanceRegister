using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Application.Exceptions;
using PAR.Contracts.Requests;
using PAR.Domain.Entities;

namespace PAR.Application.Features.SchoolYears.Commands;

public record UpdateSchoolYearCommand : IRequest<int>
{
    public int Id { get; init; }
    public UpdateSchoolYearRequest UpdateSchoolYearRequest { get; init; } = null!;
}

public class UpdatechoolYearHandler : IRequestHandler<UpdateSchoolYearCommand, int>
{
    private readonly IParDbContext _dbContext;

    public UpdatechoolYearHandler(IParDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(UpdateSchoolYearCommand request, CancellationToken cancellationToken)
    {
        var schoolYear = await _dbContext.SchoolYears.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if (schoolYear == null) 
            throw new NotFoundException(nameof(UpdateSchoolYearCommand), nameof(SchoolYear), request.Id);
        
        schoolYear.StartDate = request.UpdateSchoolYearRequest.StartDate;
        schoolYear.EndDate = request.UpdateSchoolYearRequest.EndDate;
        schoolYear.IsCurrent = request.UpdateSchoolYearRequest.IsCurrent;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return schoolYear.Id;
    }
}