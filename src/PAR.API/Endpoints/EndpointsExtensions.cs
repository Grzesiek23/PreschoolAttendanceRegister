using PAR.API.Endpoints.Account;

namespace PAR.API.Endpoints;

public static class EndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapAccountEndpoints();
        return app;
    }
}