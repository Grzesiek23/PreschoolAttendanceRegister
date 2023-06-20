using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Application.Extensions;
using PAR.Application.Mapping;
using PAR.Contracts;
using PAR.Contracts.Dtos;
using PAR.Contracts.Requests.Options;

namespace PAR.Application.Features.Preschoolers.Queries;

public record GetPreschoolersWithDetailsQuery : IRequest<PagedResponse<PreschoolerDetailsDto>>
{
    public GetPreschoolersOptionsRequest GetPreschoolersOptionsRequest { get; init; } = null!;
}

public class GetPreschoolersWithDetailsHandler : IRequestHandler<GetPreschoolersWithDetailsQuery, PagedResponse<PreschoolerDetailsDto>>
{
    private readonly IParDbContext _dbContext;

    public GetPreschoolersWithDetailsHandler(IParDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<PreschoolerDetailsDto>> Handle(GetPreschoolersWithDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.Preschoolers.Where(x => x.IsActive)
            .Include(x => x.Group)
            .AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(request.GetPreschoolersOptionsRequest.FirstName))
            query = query.Where(x => x.FirstName.Contains(request.GetPreschoolersOptionsRequest.FirstName));
        
        if (!string.IsNullOrWhiteSpace(request.GetPreschoolersOptionsRequest.LastName))
            query = query.Where(x => x.LastName.Contains(request.GetPreschoolersOptionsRequest.LastName));
        
        if (request.GetPreschoolersOptionsRequest.GroupId.HasValue)
            query = query.Where(x => x.GroupId == request.GetPreschoolersOptionsRequest.GroupId);
        
        var result = await query.ToPagedResponseAsync(request.GetPreschoolersOptionsRequest, entity => entity.AsDetailDto(),
            cancellationToken);
        
        return result;
    }
}