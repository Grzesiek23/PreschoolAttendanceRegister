using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Application.Exceptions;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Preschoolers.Commands;

public record DeletePreschoolerCommand : IRequest<Unit>
{
    public int? Id { get; init; }
}

public class DeletePreschoolerHandler : IRequestHandler<DeletePreschoolerCommand, Unit>
{
    private readonly IParDbContext _dbContext;

    public DeletePreschoolerHandler(IParDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeletePreschoolerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Preschoolers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive, cancellationToken);
        
        if (entity == null)
            throw new NotFoundException(nameof(DeletePreschoolerCommand), nameof(Preschooler), request.Id);
        
        _dbContext.Preschoolers.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}