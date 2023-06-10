using PAR.Domain.Entities;

namespace PAR.Application.DataValidation;

public interface IDataValidationService
{
    Task<(ApplicationUser teacher, SchoolYear schoolYear)> GetTeacherAndSchoolYearAsync(int teacherId, int schoolYearId,
        CancellationToken cancellationToken);
}