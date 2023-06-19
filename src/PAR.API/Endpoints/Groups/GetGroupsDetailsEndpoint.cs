using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Groups.Queries;
using PAR.Contracts;
using PAR.Contracts.Dtos;
using PAR.Contracts.Requests.Options;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Groups;

public static class GetGroupsDetailsEndpoint
{
    private const string Name = "GetGroupsDetails";

    public static IEndpointRouteBuilder MapGetGroupsDetailsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Groups.GetAllWithDetails, async ([AsParameters] GetGroupsOptionsRequest options, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var result = await mediator.Send(new GetGroupsWithDetailsQuery {GetGroupsOptionsRequest = options}, cancellationToken);
                    return TypedResults.Ok(result);
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Groups.Tag)
            .Produces<PagedResponse<GroupDetailDto>>()
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        return app;
    }
}