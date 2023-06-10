using MediatR;
using PAR.Application.DataAccessLayer;
using PAR.Application.DataValidation;
using PAR.Application.Mapping;
using PAR.Contracts.Requests;

namespace PAR.Application.Features.Preschoolers.Commands;

public record CreatePreschoolerCommand : IRequest<int>
{
    public PreschoolerRequest PreschoolerRequest { get; init; } = null!;
}

public class CreatePreschoolerHandler : IRequestHandler<CreatePreschoolerCommand, int>
{
    private readonly IParDbContext _dbContext;
    private readonly IDataValidationService _dataValidationService;

    public CreatePreschoolerHandler(IParDbContext dbContext, IDataValidationService dataValidationService)
    {
        _dbContext = dbContext;
        _dataValidationService = dataValidationService;
    }

    public async Task<int> Handle(CreatePreschoolerCommand request, CancellationToken cancellationToken)
    {
        await _dataValidationService.GetGroupAsync(request.PreschoolerRequest.GroupId, cancellationToken);
        
        var entity = request.PreschoolerRequest.AsEntity();
        
        await _dbContext.Preschoolers.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return entity.Id;
    }
}