using MediatR;
using PAR.Application.DataAccessLayer;
using PAR.Application.Extensions;
using PAR.Application.Mapping;
using PAR.Contracts;
using PAR.Contracts.Dtos;
using PAR.Contracts.Requests.Options;

namespace PAR.Application.Features.Groups.Queries;

public record GetGroupsQuery : IRequest<PagedResponse<GroupDto>>
{
    public GetGroupsOptionsRequest GetGroupsOptionsRequest { get; init; } = null!;
}

public class GetGroupsHandler : IRequestHandler<GetGroupsQuery, PagedResponse<GroupDto>>
{
    private readonly IParDbContext _dbContext;

    public GetGroupsHandler(IParDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<GroupDto>> Handle(GetGroupsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.Groups.Where(x => x.IsActive).AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(request.GetGroupsOptionsRequest.Name))
            query = query.Where(x => x.Name.Contains(request.GetGroupsOptionsRequest.Name));
        
        if (request.GetGroupsOptionsRequest.SchoolYearId.HasValue)
            query = query.Where(x => x.SchoolYearId == request.GetGroupsOptionsRequest.SchoolYearId);
        
        if (request.GetGroupsOptionsRequest.TeacherId.HasValue)
            query = query.Where(x => x.TeacherId == request.GetGroupsOptionsRequest.TeacherId);
        
        var result = await query.ToPagedResponseAsync(request.GetGroupsOptionsRequest, entity => entity.AsDto(),
            cancellationToken);
        
        return result;
    }
}