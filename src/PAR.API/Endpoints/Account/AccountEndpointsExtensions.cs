namespace PAR.API.Endpoints.Account;

public static class AccountEndpointsExtensions
{
    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapLoginEndpoint();
        return app;
    }
}