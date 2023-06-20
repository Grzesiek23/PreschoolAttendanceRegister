using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Contracts.Dtos;

namespace PAR.Application.Features.Users.Queries;

public record GetUsersAsOptionListQuery : IRequest<IEnumerable<NumberListDto>>;

public class GetUsersAsOptionListHandler : IRequestHandler<GetUsersAsOptionListQuery, IEnumerable<NumberListDto>>
{
    private readonly IParDbContext _dbContext;

    public GetUsersAsOptionListHandler(IParDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<NumberListDto>> Handle(GetUsersAsOptionListQuery request,
        CancellationToken cancellationToken)
    {
        var query = await _dbContext.ApplicationUsers.ToListAsync(cancellationToken);
        
       var result = query.Select(x => new NumberListDto
        {
            Id = x.Id,
            Name = x.FullName
        });
        
        return result;
    }
}