using SettlementManager.Web;
using SettlementManager.Web.Components;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddWeb();

WebApplication app = builder.Build();

app.UseAntiforgery();
app
    .MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();