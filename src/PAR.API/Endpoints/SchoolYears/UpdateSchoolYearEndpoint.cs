using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.SchoolYears.Commands;
using PAR.Contracts.Requests;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.SchoolYears;

public static class UpdateSchoolYearEndpoint
{
    private const string Name = "UpdateSchoolYear";

    public static IEndpointRouteBuilder MapUpdateSchoolYearEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.SchoolYears.Update,
                async (int id, UpdateSchoolYearRequest request, IMediator mediator,
                    CancellationToken cancellationToken) =>
                {
                    await mediator.Send(new UpdateSchoolYearCommand {Id = id, UpdateSchoolYearRequest = request},
                        cancellationToken);
                    return Results.NoContent();
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.SchoolYears.Tag)
            .Produces<string>(StatusCodes.Status204NoContent)
            .Produces<string>(StatusCodes.Status404NotFound)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .RequireAuthorization();
        return app;
    }
}