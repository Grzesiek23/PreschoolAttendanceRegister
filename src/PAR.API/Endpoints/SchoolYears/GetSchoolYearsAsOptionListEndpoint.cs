using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.SchoolYears.Queries;
using PAR.Contracts.Dtos;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.SchoolYears;

public static class GetSchoolYearsAsOptionListEndpoint
{
    private const string Name = "GetSchoolYearsAsOptionList";

    public static IEndpointRouteBuilder MapGetSchoolYearsAsOptionListEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.SchoolYears.GetAllAsOptionList, async (IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var result = await mediator.Send(new GetSchoolYearsAsOptionListQuery(), cancellationToken);
                    return TypedResults.Ok(result);
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.SchoolYears.Tag)
            .Produces<IEnumerable<NumberListDto>>()
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        return app;
    }
}