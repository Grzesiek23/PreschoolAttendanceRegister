using MediatR;
using PAR.Application.DataAccessLayer;
using PAR.Application.Mapping;
using PAR.Contracts.Requests;

namespace PAR.Application.Features.SchoolYears.Commands;

public record CreateSchoolYearCommand : IRequest<string>
{
    public SchoolYearRequest SchoolYearRequest { get; init; } = null!;
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
        var entity = request.SchoolYearRequest.AsEntity();

        await _dbContext.SchoolYears.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return entity.Id.ToString();
    }
}