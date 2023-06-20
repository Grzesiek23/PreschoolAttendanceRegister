namespace PAR.API.Endpoints.SchoolYears;

public static class SchoolYearEndpointExtensions
{
    public static IEndpointRouteBuilder MapSchoolYearEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateSchoolYearEndpoint();
        app.MapGetSchoolYearEndpoint();
        app.MapUpdateSchoolYearEndpoint();
        app.MapDeleteSchoolYearEndpoint();
        app.MapGetSchoolYearsEndpoint();
        app.MapGetSchoolYearsAsOptionListEndpoint();
        return app;
    }
}