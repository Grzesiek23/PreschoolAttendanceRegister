using MediatR;
using PAR.API.Constants;
using PAR.API.Helpers;
using PAR.Application.Features.Groups.Queries;
using PAR.Contracts.Dtos;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Groups;

public static class GetGroupEndpoint
{
    public const string Name = "GetGroupById";

    public static IEndpointRouteBuilder MapGetGroupEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Groups.Get, async (int id, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var result = await mediator.Send(new GetGroupByIdQuery {Id = id}, cancellationToken);
                    return ResultHelper.CheckAndReturnResult(result);
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Groups.Tag)
            .Produces<RoleDto>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .RequireAuthorization();

        return app;
    }
}