using PAR.API.Constants;
using PAR.Application.Abstractions;
using Serilog;

namespace PAR.API.Endpoints.Account;

public static class LoginEndpoint
{
    private const string Name = "Login";
    public static IEndpointRouteBuilder MapLoginEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Account.Login, async (IClock clock, ILogger logger, CancellationToken cancellationToken) =>
        {
            logger.Information("Waiting 2 seconds...");
            await Task.Delay(2000, cancellationToken);

            return TypedResults.Ok($"Hello World! {clock.Current()}");

        }).WithName(Name);

        return app;
    }
}