using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PAR.Application.Abstractions;
using PAR.Application.Configuration;
using PAR.Infrastructure.Constants;
using PAR.Infrastructure.Time;

namespace PAR.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ParSettings>(configuration.GetSection(ConfigurationConstants.ParSettings));
        services.AddSingleton<IClock, Clock>();
        return services;
    }
}