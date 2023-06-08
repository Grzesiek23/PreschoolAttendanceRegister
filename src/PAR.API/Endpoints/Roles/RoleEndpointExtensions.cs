namespace PAR.API.Endpoints.Roles;

public static class RoleEndpointExtensions
{
    public static IEndpointRouteBuilder MapRoleEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGetRoleEndpoint();
        app.MapCreateRoleEndpoint();
        return app;
    } 
}

