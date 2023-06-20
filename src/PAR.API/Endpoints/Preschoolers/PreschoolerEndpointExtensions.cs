namespace PAR.API.Endpoints.Preschoolers;

public static class PreschoolerEndpointExtensions
{
    public static IEndpointRouteBuilder MapPreschoolerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreatePreschoolerEndpoint();
        app.MapGetPreschoolerEndpoint();
        app.MapUpdatePreschoolerEndpoint();
        app.MapDeletePreschoolerEndpoint();
        app.MapGetPreschoolersEndpoint();
        app.MapGetPreschoolersDetailsEndpoint();
        return app;
    }
}