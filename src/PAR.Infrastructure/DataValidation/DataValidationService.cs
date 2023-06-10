using Microsoft.EntityFrameworkCore;
using PAR.Application.DataAccessLayer;
using PAR.Application.DataValidation;
using PAR.Application.Exceptions;
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
            throw new NotFoundException(nameof(GetTeacherAndSchoolYearAsync), nameof(ApplicationUser), teacherId);
        
        var schoolYear = await _dbContext.SchoolYears.FirstOrDefaultAsync(x => x.Id == schoolYearId && x.IsActive, cancellationToken);
        if (schoolYear is null)
            throw new NotFoundException(nameof(GetTeacherAndSchoolYearAsync), nameof(SchoolYear), schoolYearId);
        
        return (teacher, schoolYear);
    }

    public async Task<Group> GetGroupAsync(int groupId, CancellationToken cancellationToken)
    {
        var group = await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == groupId && x.IsActive, cancellationToken);
        if (group is null)
            throw new NotFoundException(nameof(GetGroupAsync), nameof(Group), groupId);
        
        return group;
    }
}