using MediatR;
using Microsoft.AspNetCore.Identity;
using PAR.Application.Exceptions;
using PAR.Contracts.Requests;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Users.Commands;

public record CreateApplicationUserCommand : IRequest<int>
{
    public ApplicationUserRequest ApplicationUserRequest { get; init; }
}

public class CreateApplicationUserHandler : IRequestHandler<CreateApplicationUserCommand, int>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public CreateApplicationUserHandler(UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<int> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
    {
        if (request.ApplicationUserRequest.Password != request.ApplicationUserRequest.ConfirmPassword)
            throw new BadRequestException(nameof(CreateApplicationUserCommand), "Passwords do not match");

        var user = new ApplicationUser
        {
            UserName = request.ApplicationUserRequest.Email,
            FirstName = request.ApplicationUserRequest.FirstName,
            LastName = request.ApplicationUserRequest.LastName,
            Email = request.ApplicationUserRequest.Email,
            PhoneNumber = request.ApplicationUserRequest.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, request.ApplicationUserRequest.Password);

        if (!result.Succeeded)
            throw new BadRequestException(nameof(CreateApplicationUserCommand),
                string.Join(", ", result.Errors.Select(x => x.Description)));

        var role = await _roleManager.FindByIdAsync(request.ApplicationUserRequest.Role.ToString());

        if (role == null)
            throw new NotFoundException(nameof(CreateApplicationUserCommand), nameof(ApplicationRole),
                request.ApplicationUserRequest.Role);

        await _userManager.AddToRoleAsync(user, role.Name!);

        return user.Id;
    }
}