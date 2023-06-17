using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Application.Extensions;
using PAR.Application.Mapping;
using PAR.Contracts;
using PAR.Contracts.Dtos;
using PAR.Contracts.Requests.Options;

namespace PAR.Application.Features.Users.Queries;

public record GetAllUsersQuery : IRequest<PagedResponse<UserDto>>
{
    public GetUsersOptionsRequest GetUsersOptionsRequest { get; init; } = new();
}

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, PagedResponse<UserDto>>
{
    private readonly IParDbContext  _parDbContext;    
    public GetAllUsersHandler(IParDbContext parDbContext)
    {
        _parDbContext = parDbContext;
    }

    public async Task<PagedResponse<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _parDbContext.ApplicationUsers.Include(x => x.UserRoles)
            .ThenInclude(d => d.ApplicationRole).AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.GetUsersOptionsRequest.FirstName))
            query = query.Where(x => x.FirstName.Contains(request.GetUsersOptionsRequest.FirstName));
        
        if (!string.IsNullOrWhiteSpace(request.GetUsersOptionsRequest.LastName))
            query = query.Where(x => x.LastName.Contains(request.GetUsersOptionsRequest.LastName));
        
        if (!string.IsNullOrWhiteSpace(request.GetUsersOptionsRequest.Email))
            query = query.Where(x => x.Email.Contains(request.GetUsersOptionsRequest.Email));
        
        var result = await query.ToPagedResponseAsync(request.GetUsersOptionsRequest, entity => entity.AsDto(),
            cancellationToken);
        
        return result;
    }
}