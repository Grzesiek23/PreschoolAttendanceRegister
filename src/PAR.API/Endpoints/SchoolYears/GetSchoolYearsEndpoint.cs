using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.SchoolYears.Queries;
using PAR.Contracts;
using PAR.Contracts.Dtos;
using PAR.Contracts.Requests.Options;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.SchoolYears;

public static class GetSchoolYearsEndpoint
{
    private const string Name = "GetSchoolYears";

    public static IEndpointRouteBuilder MapGetSchoolYearsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.SchoolYears.GetAll, async ([AsParameters] GetSchoolYearsOptionsRequest options, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var result = await mediator.Send(new GetSchoolYearsQuery {GetSchoolYearsOptionsRequest = options}, cancellationToken);
                    return TypedResults.Ok(result);
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.SchoolYears.Tag)
            .Produces<PagedResponse<SchoolYearDto>>()
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        return app;
    }
}