using MediatR;
using Microsoft.AspNetCore.Mvc;
using PAR.API.Constants;
using PAR.Application.Features.Users.Commands;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Users;

public static class RemoveUserFromRoleEndpoint
{
    private const string Name = "RemoveUserToRole";

    public static IEndpointRouteBuilder MapRemoveUserFromRoleEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.Users.RemoveUserFromRole, async ([FromRoute] string userId, [FromRoute] string roleId, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    await mediator.Send(new RemoveUserFromRoleCommand {UserId = userId, RoleId = roleId}, cancellationToken);
                    return Results.NoContent();
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Users.Tag)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest);

        return app;
    }
}