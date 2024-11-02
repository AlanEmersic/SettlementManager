using Microsoft.Extensions.DependencyInjection;

namespace SettlementManager.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}