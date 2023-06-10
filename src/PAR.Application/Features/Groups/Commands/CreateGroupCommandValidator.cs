using FluentValidation;

namespace PAR.Application.Features.Groups.Commands;

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(x => x.GroupRequest.Name)
            .NotEmpty()
            .WithMessage("Name is required.");
        
        RuleFor(x => x.GroupRequest.TeacherId)
            .NotEmpty()
            .WithMessage("TeacherId is required.");
        
        RuleFor(x => x.GroupRequest.SchoolYearId)
            .NotEmpty()
            .WithMessage("SchoolYearId is required.");
    }
}