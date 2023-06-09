using FluentValidation;

namespace PAR.Application.Features.SchoolYears.Commands;

public class CreateSchoolYearCommandValidator : AbstractValidator<CreateSchoolYearCommand>
{
    public CreateSchoolYearCommandValidator()
    {
        RuleFor(x => x.CreateSchoolYearRequest.StartDate)
            .NotEmpty()
            .WithMessage("Start date is required.");
        
        RuleFor(x => x.CreateSchoolYearRequest.EndDate)
            .NotEmpty()
            .WithMessage("End date is required.");
        
        RuleFor(x => x.CreateSchoolYearRequest.StartDate)
            .LessThan(x => x.CreateSchoolYearRequest.EndDate)
            .WithMessage("Start date must be less than end date.");
    }
}