using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Contracts.Dtos;

namespace PAR.Application.Features.Groups.Queries;

public record GetGroupsAsOptionListQuery : IRequest<IEnumerable<NumberListDto>>;

public class GetGroupsAsOptionListHandler : IRequestHandler<GetGroupsAsOptionListQuery, IEnumerable<NumberListDto>>
{
    private readonly IParDbContext _dbContext;

    public GetGroupsAsOptionListHandler(IParDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<NumberListDto>> Handle(GetGroupsAsOptionListQuery request,
        CancellationToken cancellationToken)
    {
        var query = await _dbContext.Groups.Where(x => x.IsActive).ToListAsync(cancellationToken);
        
       var result = query.Select(x => new NumberListDto
        {
            Id = x.Id,
            Name = x.Name
        });
        
        return result;
    }
}