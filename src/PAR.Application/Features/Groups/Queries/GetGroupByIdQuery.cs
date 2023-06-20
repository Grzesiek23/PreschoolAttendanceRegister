using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Application.Mapping;
using PAR.Contracts.Dtos;

namespace PAR.Application.Features.Groups.Queries;

public record GetGroupByIdQuery : IRequest<GroupDto?>
{
    public int Id { get; init; }
}

public class GetGroupByIdHandler : IRequestHandler<GetGroupByIdQuery, GroupDto?>
{
    private readonly IParDbContext _dbContext;

    public GetGroupByIdHandler(IParDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GroupDto?> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Groups
            .Include(x => x.Teacher)
            .Include(x => x.SchoolYear)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive, cancellationToken);

        return entity?.AsDto();
    }
}