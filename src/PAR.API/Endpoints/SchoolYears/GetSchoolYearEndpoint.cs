using MediatR;
using PAR.API.Constants;
using PAR.API.Helpers;
using PAR.Application.Features.SchoolYears.Queries;
using PAR.Contracts.Dtos;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.SchoolYears;

public static class GetSchoolYearEndpoint
{
    public const string Name = "GetSchoolYearById";

    public static IEndpointRouteBuilder MapGetSchoolYearEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.SchoolYears.Get, async (string id, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var result = await mediator.Send(new GetSchoolYearByIdQuery {Id = id}, cancellationToken);
                    return ResultHelper.CheckAndReturnResult(result);
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.SchoolYears.Tag)
            .Produces<RoleDto>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest);

        return app;
    }
}