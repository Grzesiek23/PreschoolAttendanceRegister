using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Application.Mapping;
using PAR.Contracts.Dtos;

namespace PAR.Application.Features.Preschoolers.Queries;

public record GetPreschoolerByIdQuery : IRequest<PreschoolerDto?>
{
    public int Id { get; init; }
}

public class GetPreschoolerByIdHandler : IRequestHandler<GetPreschoolerByIdQuery, PreschoolerDto?>
{
    private readonly IParDbContext _dbContext;

    public GetPreschoolerByIdHandler(IParDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PreschoolerDto?> Handle(GetPreschoolerByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Preschoolers.FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive, cancellationToken);

        return entity?.AsDto();
    }
}