using FluentValidation;

namespace PAR.Application.Features.Groups.Commands;

public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
{
    public UpdateGroupCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
        
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