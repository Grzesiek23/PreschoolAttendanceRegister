using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Preschoolers.Queries;
using PAR.Contracts;
using PAR.Contracts.Dtos;
using PAR.Contracts.Requests.Options;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Preschoolers;

public static class GetPreschoolersEndpoint
{
    private const string Name = "GetPreschoolers";

    public static IEndpointRouteBuilder MapGetPreschoolersEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Preschoolers.GetAll, async ([AsParameters] GetPreschoolersOptionsRequest options, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var result = await mediator.Send(new GetPreschoolersQuery {GetPreschoolersOptionsRequest = options}, cancellationToken);
                    return TypedResults.Ok(result);
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Preschoolers.Tag)
            .Produces<PagedResponse<PreschoolerDto>>()
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        return app;
    }
}