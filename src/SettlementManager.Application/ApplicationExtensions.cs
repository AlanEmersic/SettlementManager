using Microsoft.Extensions.DependencyInjection;

namespace SettlementManager.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining(typeof(ApplicationExtensions)));

        return services;
    }
}