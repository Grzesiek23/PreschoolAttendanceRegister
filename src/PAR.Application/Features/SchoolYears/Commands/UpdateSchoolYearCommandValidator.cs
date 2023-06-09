using FluentValidation;

namespace PAR.Application.Features.SchoolYears.Commands;

public class UpdateSchoolYearCommandValidator : AbstractValidator<UpdateSchoolYearCommand>
{
    public UpdateSchoolYearCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
        
        RuleFor(x => x.UpdateSchoolYearRequest.StartDate)
            .NotEmpty()
            .WithMessage("Start date is required.");
        
        RuleFor(x => x.UpdateSchoolYearRequest.EndDate)
            .NotEmpty()
            .WithMessage("End date is required.");
        
        RuleFor(x => x.UpdateSchoolYearRequest.StartDate)
            .LessThan(x => x.UpdateSchoolYearRequest.EndDate)
            .WithMessage("Start date must be less than end date.");
    }
}