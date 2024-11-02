using SettlementManager.Application;
using SettlementManager.Domain;
using SettlementManager.Infrastructure;
using SettlementManager.Web.Components;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDomain()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

app.UseInfrastructure();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();