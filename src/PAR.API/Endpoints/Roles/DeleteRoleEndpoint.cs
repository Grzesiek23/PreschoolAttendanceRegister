using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Roles.Commands;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Roles;

public static class DeleteRoleEndpoint
{
    private const string Name = "DeleteRole";

    public static IEndpointRouteBuilder MapDeleteRoleEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.Roles.Delete,
                async (string id, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    await mediator.Send(new DeleteRoleCommand {Id = id}, cancellationToken);

                    return Results.NoContent();
                }).WithName(Name)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }
}