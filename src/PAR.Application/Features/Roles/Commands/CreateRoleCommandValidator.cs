using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Roles.Commands;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator(RoleManager<ApplicationRole> roleManager)
    {
        RuleFor(x => x.RoleRequest.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(3, 256).WithMessage("Name must be between 3 and 256 characters")
            .MustAsync(async (name, _) =>
            {
                var role = await roleManager.FindByNameAsync(name);
                return role == null;
            })
            .WithMessage("Role already exists");
    }
}