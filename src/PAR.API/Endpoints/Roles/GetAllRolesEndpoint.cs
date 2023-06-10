using MediatR;
using PAR.API.Constants;
using PAR.API.Helpers;
using PAR.Application.Features.Roles.Queries;
using PAR.Contracts.Dtos;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Roles;

public static class GetAllRolesEndpoint
{
    private const string Name = "GetAllRoles";

    public static IEndpointRouteBuilder MapGetAllRolesEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Roles.GetAll, async (IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var result = await mediator.Send(new GetAllRolesQuery(), cancellationToken);
                    return ResultHelper.CheckAndReturnResult(result);
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Roles.Tag)
            .Produces<IEnumerable<RoleDto>>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .RequireAuthorization();

        return app;
    }
}