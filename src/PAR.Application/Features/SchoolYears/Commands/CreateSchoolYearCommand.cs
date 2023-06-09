using MediatR;
using PAR.Application.DataAccessLayer;
using PAR.Contracts.Requests;
using PAR.Domain.Entities;

namespace PAR.Application.Features.SchoolYears.Commands;

public record CreateSchoolYearCommand : IRequest<string>
{
    public CreateSchoolYearRequest CreateSchoolYearRequest { get; init; } = null!;
}

public class CreateSchoolYearHandler : IRequestHandler<CreateSchoolYearCommand, string>
{
    private readonly IParDbContext _dbContext;

    public CreateSchoolYearHandler(IParDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Handle(CreateSchoolYearCommand request, CancellationToken cancellationToken)
    {
        var schoolYear = new SchoolYear
        {
            StartDate = request.CreateSchoolYearRequest.StartDate,
            EndDate = request.CreateSchoolYearRequest.EndDate,
            IsCurrent = request.CreateSchoolYearRequest.IsCurrent
        };

        await _dbContext.SchoolYears.AddAsync(schoolYear, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return schoolYear.Id.ToString();
    }
}