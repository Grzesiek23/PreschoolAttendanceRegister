using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Users.Commands;

public class CreateApplicationUserCommandValidator : AbstractValidator<CreateApplicationUserCommand>
{
    public CreateApplicationUserCommandValidator(RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager)
    {
        RuleFor(x => x.ApplicationUserRequest.Email)
            .EmailAddress().WithMessage("Email is not valid");
        
        RuleFor(x => x.ApplicationUserRequest.Email)
            .MustAsync(async (email, _) =>
            {
                var user = await userManager.FindByEmailAsync(email);
                return user == null;
            })
            .WithMessage("Email already exists");

        RuleFor(x => x.ApplicationUserRequest.Role)
            .NotEmpty()
            .MustAsync(async (id, _) =>
            {
                var role = await roleManager.FindByIdAsync(id.ToString());
                return role != null;
            })
            .WithMessage("Role does not exist");

        RuleFor(x => x.ApplicationUserRequest.FirstName).NotEmpty();
        RuleFor(x => x.ApplicationUserRequest.LastName).NotEmpty();
        RuleFor(x => x.ApplicationUserRequest.Password).NotEmpty();
        RuleFor(x => x.ApplicationUserRequest.ConfirmPassword).NotEmpty();
        RuleFor(x => x.ApplicationUserRequest.Password)
            .Equal(x => x.ApplicationUserRequest.ConfirmPassword)
            .WithMessage("Passwords do not match");
    }
}