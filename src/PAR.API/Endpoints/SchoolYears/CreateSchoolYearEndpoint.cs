using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.SchoolYears.Commands;
using PAR.Contracts.Requests;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.SchoolYears;

public static class CreateSchoolYearEndpoint
{
    private const string Name = "CreateSchoolYear";

    public static IEndpointRouteBuilder MapCreateSchoolYearEndpoint(this IEndpointRouteBuilder app)
    {
        
        app.MapPost(ApiEndpoints.SchoolYears.Create,
                async (SchoolYearRequest request, IMediator mediator,
                    CancellationToken cancellationToken) =>
                {
                    var id = await mediator.Send(new CreateSchoolYearCommand {SchoolYearRequest = request},
                        cancellationToken);
                    return TypedResults.CreatedAtRoute(id, GetSchoolYearEndpoint.Name, new {id});
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.SchoolYears.Tag)
            .Produces<int>(StatusCodes.Status201Created)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();
        return app;
    }
}