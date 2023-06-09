﻿using MediatR;
using PAR.API.Constants;
using PAR.Application.Features.Roles.Commands;
using PAR.Contracts.Requests;
using PAR.Contracts.Responses;

namespace PAR.API.Endpoints.Roles;

public static class UpdateRoleEndpoint
{
    private const string Name = "UpdateRole";

    public static IEndpointRouteBuilder MapUpdateRoleEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.Roles.Update,
                async (UpdateRoleRequest request, IMediator mediator, CancellationToken cancellationToken) =>
                {
                    await mediator.Send(new UpdateRoleCommand { UpdateRoleRequest = request}, cancellationToken);

                    return Results.NoContent();
                }).WithName(Name)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }
}