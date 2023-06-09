using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Authorization.Commands;
using PAR.Contracts.Requests;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Authorizations;

public static class LoginEndpoint
{
    private const string Name = "Login";

    public static IEndpointRouteBuilder MapLoginEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Authorization.Login,
                async (LoginRequest request, IMediator mediator,
                    CancellationToken cancellationToken) =>
                {
                    var authDto = await mediator.Send(new LoginCommand {LoginRequest = request},
                        cancellationToken);
                    return TypedResults.Ok(authDto);
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Authorization.Tag)
            .Produces<string>(StatusCodes.Status204NoContent)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status500InternalServerError);
        
        return app;
    }
}