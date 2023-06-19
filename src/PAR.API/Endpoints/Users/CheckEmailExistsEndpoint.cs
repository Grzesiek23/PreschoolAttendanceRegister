using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Users.Queries;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Users;

public static class CheckEmailExistsEndpoint
{
    private const string Name = "CheckUserEmailExists";

    public static IEndpointRouteBuilder MapCheckEmailExistsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Users.Exists,
                async (string email, IMediator mediator,
                    CancellationToken cancellationToken) =>
                {
                    var result = await mediator.Send(new CheckIfUserEmailExistsQuery {Email = email},
                        cancellationToken);
                    return Results.Ok(result);
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Users.Tag)
            .Produces<string>()
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();
        
        return app;
    }
}