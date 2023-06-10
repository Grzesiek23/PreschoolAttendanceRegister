using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Application.DataValidation;
using PAR.Application.Exceptions;
using PAR.Application.Mapping;
using PAR.Contracts.Requests;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Groups.Commands;

public record CreateGroupCommand : IRequest<string>
{
    public GroupRequest GroupRequest { get; init; } = null!;
}

public class CreateGroupHandler : IRequestHandler<CreateGroupCommand, string>
{
    private readonly IParDbContext _dbContext;
    private readonly IDataValidationService _dataValidationService;

    public CreateGroupHandler(IParDbContext dbContext, IDataValidationService dataValidationService)
    {
        _dbContext = dbContext;
        _dataValidationService = dataValidationService;
    }

    public async Task<string> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        await _dataValidationService.GetTeacherAndSchoolYearAsync(request.GroupRequest.TeacherId, request.GroupRequest.SchoolYearId, cancellationToken);
        
        var entity = request.GroupRequest.AsEntity();

        await _dbContext.Groups.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return entity.Id.ToString();
    }
}