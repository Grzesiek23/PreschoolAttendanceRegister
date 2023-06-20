using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Users.Queries;
using PAR.Contracts.Dtos;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Users;

public static class GetUsersAsOptionListEndpoint
{
    private const string Name = "GetUsersAsOptionList";

    public static IEndpointRouteBuilder MapGetUsersAsOptionListEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Users.GetAllAsOptionList, async (IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var result = await mediator.Send(new GetUsersAsOptionListQuery(), cancellationToken);
                    return TypedResults.Ok(result);
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Users.Tag)
            .Produces<IEnumerable<NumberListDto>>()
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        return app;
    }
}