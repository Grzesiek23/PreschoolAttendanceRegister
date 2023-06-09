using MediatR;
using Microsoft.AspNetCore.Mvc;
using PAR.API.Constants;
using PAR.API.Helpers;
using PAR.Application.Features.Users.Commands;
using PAR.Application.Features.Users.Queries;
using PAR.Contracts.Dtos;
using PAR.Contracts.Requests;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Users;

public static class AssignUserToRoleEndpoint
{
    private const string Name = "AssignUserToRole";

    public static IEndpointRouteBuilder MapAssignUserToRoleEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.Users.AssignUserToRole, async ([FromRoute] string userId, [FromRoute] string roleId, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    await mediator.Send(new AssignUserToRoleCommand {UserId = userId, RoleId = roleId}, cancellationToken);
                    return Results.NoContent();
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Users.Tag)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest);

        return app;
    }
}