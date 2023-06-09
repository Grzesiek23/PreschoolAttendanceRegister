using FluentValidation;

namespace PAR.Application.Features.SchoolYears.Commands;

public class DeleteSchoolYearCommandValidator : AbstractValidator<DeleteSchoolYearCommand>
{
    public DeleteSchoolYearCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}