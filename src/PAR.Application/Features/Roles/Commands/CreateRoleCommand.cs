﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using PAR.Application.Exceptions;
using PAR.Contracts.Requests;
using PAR.Domain.Entities;

namespace PAR.Application.Features.Roles.Commands;

public record CreateRoleCommand : IRequest<string>
{
    public CreateRoleRequest CreateRoleRequest { get; init; } = null!;
}

public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, string>
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public CreateRoleHandler(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var newRole = new ApplicationRole
        {
            Name = request.CreateRoleRequest.Name,
        };

        var result = await _roleManager.CreateAsync(newRole);

        if (result.Succeeded) return newRole.Id;
        
        var errors = result.Errors.Select(x => x.Description);
        throw new InternalApplicationError(nameof(CreateRoleCommand), $"Failed to create role: {string.Join(",", errors)}");
    }
}