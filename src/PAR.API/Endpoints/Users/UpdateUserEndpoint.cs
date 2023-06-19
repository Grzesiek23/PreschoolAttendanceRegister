using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Users.Commands;
using PAR.Contracts.Requests;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Users;

public static class UpdateUserEndpoint
{
    private const string Name = "UpdateUser";

    public static IEndpointRouteBuilder MapUpdateUserEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.Users.Update,
                async (int id, ApplicationUserUpdateRequest request, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    await mediator.Send(new UpdateApplicationUserCommand { Id = id, ApplicationUserUpdateRequest = request}, cancellationToken);

                    return Results.NoContent();
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Users.Tag)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        return app;
    }
}