using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Application.Extensions;
using PAR.Application.Mapping;
using PAR.Contracts;
using PAR.Contracts.Dtos;
using PAR.Contracts.Requests.Options;

namespace PAR.Application.Features.Groups.Queries;

public record GetGroupsWithDetailsQuery : IRequest<PagedResponse<GroupDetailDto>>
{
    public GetGroupsOptionsRequest GetGroupsOptionsRequest { get; init; } = null!;
}

public class GetGroupsWithDetailsHandler : IRequestHandler<GetGroupsWithDetailsQuery, PagedResponse<GroupDetailDto>>
{
    private readonly IParDbContext _dbContext;

    public GetGroupsWithDetailsHandler(IParDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<GroupDetailDto>> Handle(GetGroupsWithDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.Groups.Where(x => x.IsActive)
            .Include(x => x.Teacher)
            .Include(x => x.SchoolYear)
            .AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(request.GetGroupsOptionsRequest.Name))
            query = query.Where(x => x.Name.Contains(request.GetGroupsOptionsRequest.Name));
        
        if (request.GetGroupsOptionsRequest.SchoolYearId.HasValue)
            query = query.Where(x => x.SchoolYearId == request.GetGroupsOptionsRequest.SchoolYearId);
        
        if (request.GetGroupsOptionsRequest.TeacherId.HasValue)
            query = query.Where(x => x.TeacherId == request.GetGroupsOptionsRequest.TeacherId);
        
        var result = await query.ToPagedResponseAsync(request.GetGroupsOptionsRequest, entity => entity.AsDetailDto(),
            cancellationToken);
        
        return result;
    }
}