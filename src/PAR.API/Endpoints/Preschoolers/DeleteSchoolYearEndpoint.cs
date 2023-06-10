using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Preschoolers.Commands;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Preschoolers;

public static class DeletePreschoolerEndpoint
{
    private const string Name = "DeletePreschooler";

    public static IEndpointRouteBuilder MapDeletePreschoolerEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.Preschoolers.Delete,
                async (int id, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    await mediator.Send(new DeletePreschoolerCommand {Id = id}, cancellationToken);

                    return Results.NoContent();
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Preschoolers.Tag)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        return app;
    }
}