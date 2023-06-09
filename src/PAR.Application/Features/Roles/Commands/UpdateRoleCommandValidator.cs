using FluentValidation;

namespace PAR.Application.Features.Roles.Commands;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(x => x.RoleRequest.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.RoleRequest.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(3, 256).WithMessage("Name must be between 3 and 256 characters");
    }
}