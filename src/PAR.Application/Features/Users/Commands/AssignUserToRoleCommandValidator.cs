using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Users.Commands;

public class AssignUserToRoleCommandValidator : AbstractValidator<AssignUserToRoleCommand>
{
    public AssignUserToRoleCommandValidator(UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .MustAsync(async (userId, _) =>
            {
                var user = await userManager.FindByIdAsync(userId);
                return user != null;
            }).WithMessage("User does not exist");

        RuleFor(x => x.RoleId)
            .NotEmpty()
            .MustAsync(async (roleId, _) =>
            {
                var role = await roleManager.FindByIdAsync(roleId);
                return role != null;
            }).WithMessage("Role does not exist");
    }
}