﻿using PAR.API.Endpoints.Accounts;
using PAR.API.Endpoints.Authorizations;
using PAR.API.Endpoints.Roles;
using PAR.API.Endpoints.Users;

namespace PAR.API.Endpoints;

public static class EndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapAccountEndpoints();
        app.MapAuthorizationEndpoints();
        app.MapUserEndpoints();
        app.MapRoleEndpoints();
        return app;
    }
}