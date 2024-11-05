using SettlementManager.Web.Services.Settlements;

namespace SettlementManager.Web;

public static class WebExtensions
{
    public static IServiceCollection AddWeb(this IServiceCollection services)
    {
        services.AddHttpClient<ISettlementService, SettlementService>(client =>
        {
            const string baseApiUri = "https://localhost:7288";
            client.BaseAddress = new Uri(baseApiUri);
        });

        services
            .AddRazorComponents()
            .AddInteractiveServerComponents();

        return services;
    }
}