using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Account.Commands;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator(UserManager<ApplicationUser> userManager)
    {
        RuleFor(x => x.CreateAccountRequest.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress()
            .MustAsync(async (email, _) =>
            {
                var user = await userManager.FindByEmailAsync(email);
                return user == null;
            }).WithMessage("User already exists");
        RuleFor(x => x.CreateAccountRequest.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.CreateAccountRequest.ConfirmPassword).NotEmpty().Equal(x => x.CreateAccountRequest.Password)
            .WithMessage("Passwords do not match");
        RuleFor(x => x.CreateAccountRequest.FirstName).NotEmpty();
        RuleFor(x => x.CreateAccountRequest.LastName).NotEmpty();
    }
}