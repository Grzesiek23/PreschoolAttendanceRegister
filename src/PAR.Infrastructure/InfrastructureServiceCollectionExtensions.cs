using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PAR.Application.Abstractions;
using PAR.Application.Configuration;
using PAR.Application.Security;
using PAR.Infrastructure.Authorization;
using PAR.Infrastructure.Constants;
using PAR.Infrastructure.Time;

namespace PAR.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ParSettings>(configuration.GetSection(ConfigurationConstants.ParSettings));
        services.Configure<JwtSettings>(configuration.GetSection(ConfigurationConstants.JwtSettings));
        services.AddSingleton<IClock, Clock>();
        services .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                var jwtSettings = configuration.GetSection(ConfigurationConstants.JwtSettings).Get<JwtSettings>();

                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}