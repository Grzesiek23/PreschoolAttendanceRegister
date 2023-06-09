using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Users.Commands;

public class RemoveUserFromRoleCommandValidator : AbstractValidator<RemoveUserFromRoleCommand>
{
    public RemoveUserFromRoleCommandValidator(UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.RoleId)
            .NotEmpty();
    }
}