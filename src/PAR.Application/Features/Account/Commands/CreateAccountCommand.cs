using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PAR.Application.Configuration;
using PAR.Application.Exceptions;
using PAR.Contracts.Requests;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Account.Commands;

public record CreateAccountCommand : IRequest<string>
{
    public CreateAccountRequest CreateAccountRequest { get; init; } = null!;
    public bool ConfirmPassword { get; init; }
    public bool AddToDefaultRole { get; init; }
}

public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ParSettings _parSettings;

    public CreateAccountHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
        IOptions<ParSettings> parSettings)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _parSettings = parSettings.Value;
    }

    public async Task<string> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var newUser = new ApplicationUser
        {
            UserName = request.CreateAccountRequest.Email,
            Email = request.CreateAccountRequest.Email,
            FirstName = request.CreateAccountRequest.FirstName!,
            LastName = request.CreateAccountRequest.LastName!
        };

        if (request.ConfirmPassword)
            newUser.EmailConfirmed = true;

        var result = await _userManager.CreateAsync(newUser, request.CreateAccountRequest.Password!);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description);
            throw new InternalApplicationError(nameof(CreateAccountCommand),
                $"Failed to create user: {string.Join(",", errors)}");
        }

        if (!request.AddToDefaultRole) return newUser.Id;

        var role = await _roleManager.FindByNameAsync(_parSettings.DefaultUserRole);
        if (role == null)
            throw new InternalApplicationError(nameof(CreateAccountCommand),
                $"Failed to find role: {_parSettings.DefaultUserRole}");

        var assignResult = await _userManager.AddToRoleAsync(newUser, _parSettings.DefaultUserRole);

        if (assignResult.Succeeded) return newUser.Id;
        {
            var errors = assignResult.Errors.Select(x => x.Description);
            throw new InternalApplicationError(nameof(CreateAccountCommand),
                $"Failed to assign user to role: {string.Join(",", errors)}");
        }
    }
}