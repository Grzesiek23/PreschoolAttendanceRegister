using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PAR.Application.Abstractions;
using PAR.Infrastructure.Time;

namespace PAR.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IClock, Clock>();
        return services;
    }
}