using MediatR;
using PAR.Application.DataAccessLayer;
using PAR.Application.Extensions;
using PAR.Application.Mapping;
using PAR.Contracts;
using PAR.Contracts.Dtos;
using PAR.Contracts.Requests.Options;

namespace PAR.Application.Features.Preschoolers.Queries;

public record GetPreschoolersQuery : IRequest<PagedResponse<PreschoolerDto>>
{
    public GetPreschoolersOptionsRequest GetPreschoolersOptionsRequest { get; init; } = null!;
}

public class GetPreschoolersHandler : IRequestHandler<GetPreschoolersQuery, PagedResponse<PreschoolerDto>>
{
    private readonly IParDbContext _dbContext;

    public GetPreschoolersHandler(IParDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<PreschoolerDto>> Handle(GetPreschoolersQuery request,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.Preschoolers.Where(x => x.IsActive).AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(request.GetPreschoolersOptionsRequest.FirstName))
            query = query.Where(x => x.FirstName.Contains(request.GetPreschoolersOptionsRequest.FirstName));
        
        if (!string.IsNullOrWhiteSpace(request.GetPreschoolersOptionsRequest.LastName))
            query = query.Where(x => x.LastName.Contains(request.GetPreschoolersOptionsRequest.LastName));
        
        if (request.GetPreschoolersOptionsRequest.GroupId.HasValue)
            query = query.Where(x => x.GroupId == request.GetPreschoolersOptionsRequest.GroupId);
            
        var result = await query.ToPagedResponseAsync(request.GetPreschoolersOptionsRequest, entity => entity.AsDto(),
            cancellationToken);
        
        return result;
    }
}