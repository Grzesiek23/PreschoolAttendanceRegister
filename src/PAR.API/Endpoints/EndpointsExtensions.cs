using PAR.API.Endpoints.Accounts;
using PAR.API.Endpoints.Roles;
using PAR.API.Endpoints.Users;

namespace PAR.API.Endpoints;

public static class EndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapAccountEndpoints();
        app.MapUserEndpoints();
        app.MapRoleEndpoints();
        return app;
    }
}