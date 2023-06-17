using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Groups.Queries;
using PAR.Contracts;
using PAR.Contracts.Dtos;
using PAR.Contracts.Requests.Options;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Groups;

public static class GetGroupsEndpoint
{
    private const string Name = "GetGroups";

    public static IEndpointRouteBuilder MapGetGroupsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Groups.GetAll, async ([AsParameters] GetGroupsOptionsRequest options, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var result = await mediator.Send(new GetGroupsQuery {GetGroupsOptionsRequest = options}, cancellationToken);
                    return TypedResults.Ok(result);
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Groups.Tag)
            .Produces<PagedResponse<GroupDto>>()
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        return app;
    }
}