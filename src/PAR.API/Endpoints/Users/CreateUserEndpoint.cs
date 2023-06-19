using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Users.Commands;
using PAR.Contracts.Requests;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Users;

public static class CreateUserEndpoint
{
    private const string Name = "CreateUser";

    public static IEndpointRouteBuilder MapCreateUserEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Users.Create,
                async (ApplicationUserRequest request, IMediator mediator,
                    CancellationToken cancellationToken) =>
                {
                    var id = await mediator.Send(new CreateApplicationUserCommand {ApplicationUserRequest = request},
                        cancellationToken);
                    return TypedResults.CreatedAtRoute(GetUserEndpoint.Name, new {id});
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Users.Tag)
            .Produces<string>(StatusCodes.Status201Created)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();
        
        return app;
    }
}