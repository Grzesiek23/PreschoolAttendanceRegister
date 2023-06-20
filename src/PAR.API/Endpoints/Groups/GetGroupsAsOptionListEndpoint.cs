using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Groups.Queries;
using PAR.Contracts.Dtos;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Groups;

public static class GetGroupsAsOptionListEndpoint
{
    private const string Name = "GetGroupsAsOptionList";

    public static IEndpointRouteBuilder MapGetGroupsAsOptionListEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Groups.GetAllAsOptionList, async (IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var result = await mediator.Send(new GetGroupsAsOptionListQuery(), cancellationToken);
                    return TypedResults.Ok(result);
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Groups.Tag)
            .Produces<IEnumerable<NumberListDto>>()
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        return app;
    }
}