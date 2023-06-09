using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Roles.Commands;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.SchoolYears;

public static class DeleteSchoolYearEndpoint
{
    private const string Name = "DeleteSchoolYear";

    public static IEndpointRouteBuilder MapDeleteSchoolYearEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.SchoolYears.Delete,
                async (string id, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    await mediator.Send(new DeleteRoleCommand {Id = id}, cancellationToken);

                    return Results.NoContent();
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.SchoolYears.Tag)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }
}