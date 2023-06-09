using MediatR;
using PAR.API.Constants;
using PAR.API.Helpers;
using PAR.Application.Features.Roles.Queries;
using PAR.Contracts.Dtos;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Roles;

public static class GetRoleEndpoint
{
    public const string Name = "GetRoleById";

    public static IEndpointRouteBuilder MapGetRoleEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Roles.Get, async (string id, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var result = await mediator.Send(new GetRoleByIdQuery {Id = id}, cancellationToken);
                    return ResultHelper.CheckAndReturnResult(result);
                }).WithName(Name)
            .Produces<RoleDto>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest);

        return app;
    }
}