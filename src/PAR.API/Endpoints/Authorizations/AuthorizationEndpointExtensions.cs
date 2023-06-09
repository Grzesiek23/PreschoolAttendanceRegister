namespace PAR.API.Endpoints.Authorizations;

public static class AuthorizationEndpointExtensions
{
    public static IEndpointRouteBuilder MapAuthorizationEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapLoginEndpoint();
        return app;
    }
}