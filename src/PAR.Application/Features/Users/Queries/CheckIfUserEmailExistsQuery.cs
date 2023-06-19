using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;

namespace PAR.Application.Features.Users.Queries;

public record CheckIfUserEmailExistsQuery : IRequest<bool>
{
    public string Email { get; init; }
}

public class CheckIfUserEmailExistsHandler : IRequestHandler<CheckIfUserEmailExistsQuery, bool>
{
    private readonly IParDbContext _context;

    public CheckIfUserEmailExistsHandler(IParDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CheckIfUserEmailExistsQuery request, CancellationToken cancellationToken)
    {
        return await _context.ApplicationUsers
            .AnyAsync(x => x.Email == request.Email, cancellationToken);
    }
}