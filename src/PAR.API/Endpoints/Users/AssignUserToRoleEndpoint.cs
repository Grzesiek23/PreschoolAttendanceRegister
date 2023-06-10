using MediatR;
using Microsoft.AspNetCore.Mvc;
using PAR.API.Constants;
using PAR.Application.Features.Users.Commands;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Users;

public static class AssignUserToRoleEndpoint
{
    private const string Name = "AssignUserToRole";

    public static IEndpointRouteBuilder MapAssignUserToRoleEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.Users.AssignUserToRole, async ([FromRoute] int userId, [FromRoute] int roleId, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    await mediator.Send(new AssignUserToRoleCommand {UserId = userId, RoleId = roleId}, cancellationToken);
                    return Results.NoContent();
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Users.Tag)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .RequireAuthorization();

        return app;
    }
}