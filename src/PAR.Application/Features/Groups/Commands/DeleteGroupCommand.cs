using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Application.Exceptions;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Groups.Commands;

public record DeleteGroupCommand : IRequest<Unit>
{
    public int? Id { get; init; }
}

public class DeleteGroupHandler : IRequestHandler<DeleteGroupCommand, Unit>
{
    private readonly IParDbContext _dbContext;

    public DeleteGroupHandler(IParDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Groups.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive, cancellationToken);
        
        if (entity == null)
            throw new NotFoundException(nameof(DeleteGroupCommand), nameof(Group), request.Id);
        
        var checkNested = await _dbContext.Preschoolers.AnyAsync(x => x.GroupId == request.Id && x.IsActive, cancellationToken);
        
        if (checkNested)
            throw new InternalApplicationError(nameof(DeleteGroupCommand),
                $"Failed to delete group: {nameof(Group)} has nested {nameof(Preschooler)}");
        
        _dbContext.Groups.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}