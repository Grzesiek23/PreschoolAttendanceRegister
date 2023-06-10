using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Application.DataValidation;
using PAR.Application.Exceptions;
using PAR.Application.Mapping;
using PAR.Contracts.Requests;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Groups.Commands;

public record UpdateGroupCommand : IRequest<int>
{
    public int Id { get; init; }
    public GroupRequest GroupRequest { get; init; } = null!;
}

public class UpdateGroupHandler : IRequestHandler<UpdateGroupCommand, int>
{
    private readonly IParDbContext _dbContext;
    private readonly IDataValidationService _dataValidationService;

    public UpdateGroupHandler(IParDbContext dbContext, IDataValidationService dataValidationService)
    {
        _dbContext = dbContext;
        _dataValidationService = dataValidationService;
    }

    public async Task<int> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        if (request.Id != request.GroupRequest.Id)
            throw new BadRequestException(nameof(UpdateGroupCommand),
                $"Id in request body ({request.GroupRequest.Id}) does not match id in request path ({request.Id})");

        await _dataValidationService.GetTeacherAndSchoolYearAsync(request.GroupRequest.TeacherId,
            request.GroupRequest.SchoolYearId, cancellationToken);

        var group = await _dbContext.Groups.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive, cancellationToken);

        if (group == null)
            throw new NotFoundException(nameof(UpdateGroupCommand), nameof(Group), request.Id);

        var entity = request.GroupRequest.AsEntity();

        _dbContext.Groups.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return group.Id;
    }
}