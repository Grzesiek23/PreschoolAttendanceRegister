using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Groups.Commands;
using PAR.Contracts.Requests;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Groups;

public static class UpdateGroupEndpoint
{
    private const string Name = "UpdateGroupYear";

    public static IEndpointRouteBuilder MapUpdateGroupEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.Groups.Update,
                async (int id, GroupRequest request, IMediator mediator,
                    CancellationToken cancellationToken) =>
                {
                    await mediator.Send(new UpdateGroupCommand {Id = id, GroupRequest = request},
                        cancellationToken);
                    return Results.NoContent();
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Groups.Tag)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .RequireAuthorization();
        
        return app;
    }
}