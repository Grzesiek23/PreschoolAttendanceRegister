using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Groups.Commands;
using PAR.Contracts.Requests;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Groups;

public static class CreateGroupEndpoint
{
    private const string Name = "CreateGroup";

    public static IEndpointRouteBuilder MapCreateGroupEndpoint(this IEndpointRouteBuilder app)
    {
        
        app.MapPost(ApiEndpoints.Groups.Create,
                async (GroupRequest request, IMediator mediator,
                    CancellationToken cancellationToken) =>
                {
                    var id = await mediator.Send(new CreateGroupCommand {GroupRequest = request},
                        cancellationToken);
                    return TypedResults.CreatedAtRoute(GetGroupEndpoint.Name, new {id});
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Groups.Tag)
            .Produces<string>(StatusCodes.Status201Created)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();
        return app;
    }
}