using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Groups.Commands;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Groups;

public static class DeleteGroupEndpoint
{
    private const string Name = "DeleteGroup";

    public static IEndpointRouteBuilder MapDeleteGroupEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.Groups.Delete,
                async (int id, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    await mediator.Send(new DeleteGroupCommand {Id = id}, cancellationToken);

                    return Results.NoContent();
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Groups.Tag)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        return app;
    }
}