using SettlementManager.Application;
using SettlementManager.Domain;
using SettlementManager.Infrastructure;
using SettlementManager.Web;
using SettlementManager.Web.Components;
using SettlementManager.Web.Services.Settlements;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    // .AddDomain()
    // .AddApplication()
    // .AddInfrastructure(builder.Configuration)
    .AddWeb();

WebApplication app = builder.Build();

// app.UseInfrastructure();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app
    .MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();