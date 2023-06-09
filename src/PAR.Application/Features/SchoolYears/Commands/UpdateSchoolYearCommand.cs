using MediatR;
using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Application.Exceptions;
using PAR.Application.Mapping;
using PAR.Contracts.Requests;
using PAR.Domain.Entities;

namespace PAR.Application.Features.SchoolYears.Commands;

public record UpdateSchoolYearCommand : IRequest<int>
{
    public int Id { get; init; }
    public SchoolYearRequest SchoolYearRequest { get; init; } = null!;
}

public class UpdateSchoolYearHandler : IRequestHandler<UpdateSchoolYearCommand, int>
{
    private readonly IParDbContext _dbContext;

    public UpdateSchoolYearHandler(IParDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(UpdateSchoolYearCommand request, CancellationToken cancellationToken)
    {
        if(request.Id != request.SchoolYearRequest.Id)
            throw new BadRequestException(nameof(UpdateSchoolYearCommand), $"Id in request body ({request.SchoolYearRequest.Id}) does not match id in request path ({request.Id})");
                
        var schoolYear = await _dbContext.SchoolYears.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive, cancellationToken);
        
        if (schoolYear == null) 
            throw new NotFoundException(nameof(UpdateSchoolYearCommand), nameof(SchoolYear), request.Id);
        
        var entity = request.SchoolYearRequest.AsEntity();
        
        _dbContext.SchoolYears.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return schoolYear.Id;
    }
}