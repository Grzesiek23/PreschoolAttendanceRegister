using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Application.Mapping;
using PAR.Contracts.Dtos;

namespace PAR.Application.Features.Users.Queries;

public record GetUserByIdQuery : IRequest<UserDto?>
{
    public int Id { get; init; }
}

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IParDbContext _parDbContext;

    public GetUserByIdHandler(IParDbContext parDbContext)
    {
        _parDbContext = parDbContext;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _parDbContext.ApplicationUsers.Include(x => x.UserRoles)
            .ThenInclude(d => d.ApplicationRole)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return user.AsDto();
    }
}