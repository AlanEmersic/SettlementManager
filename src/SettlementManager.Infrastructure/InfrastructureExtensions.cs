using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SettlementManager.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseExceptionHandler();
        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        return app;
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddHttpContextAccessor();

        services
            .AddRazorComponents()
            .AddInteractiveServerComponents();
    }
}