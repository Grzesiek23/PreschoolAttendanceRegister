using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PAR.Application.DataAccessLayer;
using PAR.Persistence.Context;

namespace PAR.Persistence;

public static class PersistenceServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ParDbConnection") ?? throw new InvalidOperationException("Connection string 'ParDbConnection' not found.");

        services.AddDbContext<ParDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IParDbContext, ParDbContext>();

        return services;
    }
}