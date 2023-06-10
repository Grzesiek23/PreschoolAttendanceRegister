using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Application.DataValidation;
using PAR.Application.Exceptions;
using PAR.Application.Features.Groups.Commands;
using PAR.Domain.Entities;

namespace PAR.Infrastructure.DataValidation;

public class DataValidationService : IDataValidationService
{
    private readonly IParDbContext _dbContext;

    public DataValidationService(IParDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(ApplicationUser teacher, SchoolYear schoolYear)> GetTeacherAndSchoolYearAsync(int teacherId, int schoolYearId, CancellationToken cancellationToken)
    {
        var teacher = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == teacherId, cancellationToken);
        if (teacher is null)
            throw new NotFoundException(nameof(CreateGroupCommand), nameof(ApplicationUser), teacherId);
        
        var schoolYear = await _dbContext.SchoolYears.FirstOrDefaultAsync(x => x.Id == schoolYearId && x.IsActive, cancellationToken);
        if (schoolYear is null)
            throw new NotFoundException(nameof(CreateGroupCommand), nameof(SchoolYear), schoolYearId);
        
        return (teacher, schoolYear);
    }
}