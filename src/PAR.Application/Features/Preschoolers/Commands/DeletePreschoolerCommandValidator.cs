using FluentValidation;

namespace PAR.Application.Features.Preschoolers.Commands;

public class DeletePreschoolerCommandValidator : AbstractValidator<DeletePreschoolerCommand>
{
    public DeletePreschoolerCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}