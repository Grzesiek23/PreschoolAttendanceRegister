using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Roles.Commands;
using PAR.Contracts.Requests;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Roles;

public static class CreateRoleEndpoint
{
    private const string Name = "CreateRole";

    public static IEndpointRouteBuilder MapCreateRoleEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Roles.Create,
                async (CreateRoleRequest request, IMediator mediator,
                    CancellationToken cancellationToken) =>
                {
                    var id = await mediator.Send(new CreateRoleCommand {CreateRoleRequest = request},
                        cancellationToken);
                    return TypedResults.CreatedAtRoute(GetRoleEndpoint.Name, new {id});
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Roles.Tag)
            .Produces<string>(StatusCodes.Status201Created)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
        
        return app;
    }
}