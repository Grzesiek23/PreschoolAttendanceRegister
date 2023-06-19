using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Users.Commands;

public class UpdateApplicationUserCommandValidator : AbstractValidator<UpdateApplicationUserCommand>
{
    public UpdateApplicationUserCommandValidator(RoleManager<ApplicationRole> roleManager)
    {
        RuleFor(x => x.ApplicationUserUpdateRequest.FirstName).NotEmpty();
        RuleFor(x => x.ApplicationUserUpdateRequest.LastName).NotEmpty();
        RuleFor(x => x.ApplicationUserUpdateRequest.RoleId)
            .NotEmpty()
            .MustAsync(async (id, _) =>
            {
                var role = await roleManager.FindByIdAsync(id.ToString());
                return role != null;
            })
            .WithMessage("Role does not exist");
    }
}