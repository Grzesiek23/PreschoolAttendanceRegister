using FluentValidation;

namespace PAR.Application.Features.Preschoolers.Commands;

public class UpdateGroupCommandValidator : AbstractValidator<UpdatePreschoolerCommand>
{
    public UpdateGroupCommandValidator()
    {
        RuleFor(x => x.PreschoolerRequest.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
        
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