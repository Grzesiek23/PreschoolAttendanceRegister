using MediatR;
using Microsoft.AspNetCore.Mvc;
using PAR.API.Constants;
using PAR.API.Helpers;
using PAR.Application.Features.Users.Queries;
using PAR.Contracts.Dtos;

namespace PAR.API.Endpoints.Users;

public static class GetUserEndpoint
{
    public const string Name = "GetUserById";

    public static IEndpointRouteBuilder MapGetUserEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Users.Get, async ([FromRoute] int id, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var result = await mediator.Send(new GetUserByIdQuery {Id = id}, cancellationToken);
                    return ResultHelper.CheckAndReturnResult(result);
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Users.Tag)
            .Produces<UserDto>()
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}