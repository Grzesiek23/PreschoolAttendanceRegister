using MediatR;
using Microsoft.AspNetCore.Mvc;
using PAR.API.Constants;
using PAR.API.Endpoints.Users;
using PAR.Application.Features.Account.Commands;
using PAR.Contracts.Requests;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Accounts;

public static class CreateAccountEndpoint
{
    private const string Name = "CreateAccount";

    public static IEndpointRouteBuilder MapCreateAccount(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Account.Create, async (CreateAccountRequest request, [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var id = await mediator.Send(new CreateAccountCommand
                {
                    CreateAccountRequest = request,
                    ConfirmPassword = true,
                    AddToDefaultRole = true
                }, cancellationToken);
                return TypedResults.CreatedAtRoute(GetUserEndpoint.Name, new {id});
            })
            .WithName(Name)
            .WithTags(ApiEndpoints.Account.Tag)
            .Produces<string>(StatusCodes.Status201Created)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        return app;
    }
}