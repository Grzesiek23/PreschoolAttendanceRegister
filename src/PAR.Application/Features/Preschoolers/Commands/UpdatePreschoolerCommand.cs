using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Application.DataValidation;
using PAR.Application.Exceptions;
using PAR.Application.Mapping;
using PAR.Contracts.Requests;

namespace PAR.Application.Features.Preschoolers.Commands;

public record UpdatePreschoolerCommand : IRequest<int>
{
    public int Id { get; init; }
    public PreschoolerRequest PreschoolerRequest { get; init; } = null!;
}

public class UpdatePreschoolerHandler : IRequestHandler<UpdatePreschoolerCommand, int>
{
    private readonly IParDbContext _dbContext;
    private readonly IDataValidationService _dataValidationService;

    public UpdatePreschoolerHandler(IParDbContext dbContext, IDataValidationService dataValidationService)
    {
        _dbContext = dbContext;
        _dataValidationService = dataValidationService;
    }

    public async Task<int> Handle(UpdatePreschoolerCommand request, CancellationToken cancellationToken)
    {
        if (request.Id != request.PreschoolerRequest.Id)
            throw new BadRequestException(nameof(UpdatePreschoolerCommand),
                $"Id in request body ({request.PreschoolerRequest.Id}) does not match id in request path ({request.Id})");

        await _dataValidationService.GetGroupAsync(request.PreschoolerRequest.GroupId, cancellationToken);

        var preschooler = await _dbContext.Preschoolers.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive, cancellationToken);

        if (preschooler == null)
            throw new NotFoundException(nameof(UpdatePreschoolerCommand), nameof(preschooler), request.Id);

        var entity = request.PreschoolerRequest.AsEntity();

        _dbContext.Preschoolers.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return preschooler.Id;
    }
}