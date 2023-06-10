using MediatR;
using PAR.API.Constants;
using PAR.API.Endpoints.Preschoolers;
using PAR.Application.Features.Preschoolers.Commands;
using PAR.Contracts.Requests;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Preschoolers;

public static class CreatePreschoolerEndpoint
{
    private const string Name = "CreatePreschooler";

    public static IEndpointRouteBuilder MapCreatePreschoolerEndpoint(this IEndpointRouteBuilder app)
    {
        
        app.MapPost(ApiEndpoints.Preschoolers.Create,
                async (PreschoolerRequest request, IMediator mediator,
                    CancellationToken cancellationToken) =>
                {
                    var id = await mediator.Send(new CreatePreschoolerCommand {PreschoolerRequest = request},
                        cancellationToken);
                    return TypedResults.CreatedAtRoute(GetPreschoolerEndpoint.Name, new {id});
                })
            .WithName(Name)
            .WithTags(ApiEndpoints.Preschoolers.Tag)
            .Produces<string>(StatusCodes.Status201Created)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();
        return app;
    }
}