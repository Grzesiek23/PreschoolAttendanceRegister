namespace PAR.API.Endpoints.SchoolYears;

public static class SchoolYearEndpointExtensions
{
    public static IEndpointRouteBuilder MapSchoolYearEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapCreateSchoolYearEndpoint();
        app.MapGetSchoolYearEndpoint();
        app.MapUpdateSchoolYearEndpoint();
        return app;
    }
}