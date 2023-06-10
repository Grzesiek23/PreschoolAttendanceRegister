using MediatR;
using PAR.API.Constants;
using PAR.API.Helpers;
using PAR.Application.Features.Preschoolers.Queries;
using PAR.Contracts.Dtos;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Preschoolers;

public static class GetPreschoolerEndpoint
{
    public const string Name = "GetPreschoolerById";

    public static IEndpointRouteBuilder MapGetPreschoolerEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Preschoolers.Get, async (int id, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var result = await mediator.Send(new GetPreschoolerByIdQuery {Id = id}, cancellationToken);
                    return ResultHelper.CheckAndReturnResult(result);
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Preschoolers.Tag)
            .Produces<PreschoolerDto>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        return app;
    }
}