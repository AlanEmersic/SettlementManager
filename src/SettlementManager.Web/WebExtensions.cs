using SettlementManager.Web.Services.Settlements;

namespace SettlementManager.Web;

public static class WebExtensions
{
    public static IServiceCollection AddWeb(this IServiceCollection services)
    {
        services.AddServices();

        services.AddHttpClient("SettlementManagerApi", client =>
        {
            const string baseApiUri = "https://localhost:7288";
            client.BaseAddress = new Uri(baseApiUri);
        });

        services
            .AddRazorComponents()
            .AddInteractiveServerComponents();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ISettlementApiService, SettlementApiService>();
        services.AddScoped<ISettlementService, SettlementService>();

        return services;
    }
}