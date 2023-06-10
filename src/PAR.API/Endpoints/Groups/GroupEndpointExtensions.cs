namespace PAR.API.Endpoints.Groups;

public static class GroupEndpointExtensions
{
    public static IEndpointRouteBuilder MapGroupEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateGroupEndpoint();
        app.MapGetGroupEndpoint();
        app.MapUpdateGroupEndpoint();
        app.MapDeleteGroupEndpoint();
        return app;
    }
}