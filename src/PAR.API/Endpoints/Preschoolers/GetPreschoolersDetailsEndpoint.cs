using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Preschoolers.Queries;
using PAR.Contracts;
using PAR.Contracts.Dtos;
using PAR.Contracts.Requests.Options;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Preschoolers;

public static class GetPreschoolersDetailsEndpoint
{
    private const string Name = "GetPreschoolersDetails";

    public static IEndpointRouteBuilder MapGetPreschoolersDetailsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Preschoolers.GetAllWithDetails, async ([AsParameters] GetPreschoolersOptionsRequest options, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var result = await mediator.Send(new GetPreschoolersWithDetailsQuery {GetPreschoolersOptionsRequest = options}, cancellationToken);
                    return TypedResults.Ok(result);
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Groups.Tag)
            .Produces<PagedResponse<PreschoolerDetailsDto>>()
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        return app;
    }
}