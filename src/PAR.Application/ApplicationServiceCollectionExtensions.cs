using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PAR.Application.Behaviors;

namespace PAR.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<IApplicationMarker>());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Transient);

        return services;
    }
}