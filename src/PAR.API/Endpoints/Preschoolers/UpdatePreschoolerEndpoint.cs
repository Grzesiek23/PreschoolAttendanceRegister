using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Preschoolers.Commands;
using PAR.Contracts.Requests;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Preschoolers;

public static class UpdatePreschoolerEndpoint
{
    private const string Name = "UpdatePreschooler";

    public static IEndpointRouteBuilder MapUpdatePreschoolerEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.Preschoolers.Update,
                async (int id, PreschoolerRequest request, IMediator mediator,
                    CancellationToken cancellationToken) =>
                {
                    await mediator.Send(new UpdatePreschoolerCommand {Id = id, PreschoolerRequest = request},
                        cancellationToken);
                    return Results.NoContent();
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Preschoolers.Tag)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .RequireAuthorization();
        
        return app;
    }
}