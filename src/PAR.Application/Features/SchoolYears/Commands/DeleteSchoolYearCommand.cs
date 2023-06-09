using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Application.Exceptions;
using PAR.Domain.Entities;

namespace PAR.Application.Features.SchoolYears.Commands;

public record DeleteSchoolYearCommand : IRequest<Unit>
{
    public int? Id { get; init; }
}

public class DeleteSchoolYearHandler : IRequestHandler<DeleteSchoolYearCommand, Unit>
{
    private readonly IParDbContext _dbContext;

    public DeleteSchoolYearHandler(IParDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeleteSchoolYearCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.SchoolYears.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if (entity == null)
            throw new NotFoundException(nameof(DeleteSchoolYearCommand), nameof(SchoolYear), request.Id);
        
        var checkNested = await _dbContext.Groups.AnyAsync(x => x.SchoolYearId == request.Id, cancellationToken);
        
        if (checkNested)
            throw new InternalApplicationError(nameof(DeleteSchoolYearCommand),
                $"Failed to delete school year: {nameof(SchoolYear)} has nested {nameof(Group)}");
        
        _dbContext.SchoolYears.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}