using FluentValidation;

namespace PAR.Application.Features.Preschoolers.Commands;

public class CreatePreschoolerCommandValidator : AbstractValidator<CreatePreschoolerCommand>
{
    public CreatePreschoolerCommandValidator()
    {
        RuleFor(x => x.PreschoolerRequest.FirstName)
            .NotEmpty()
            .WithMessage("FirstName is required.");
        
        RuleFor(x => x.PreschoolerRequest.LastName)
            .NotEmpty()
            .WithMessage("LastName is required.");
        
        RuleFor(x => x.PreschoolerRequest.GroupId)
            .NotEmpty()
            .WithMessage("GroupId is required.");
    }
}