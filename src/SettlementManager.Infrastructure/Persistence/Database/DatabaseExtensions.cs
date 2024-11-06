using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SettlementManager.Application.Common.Interfaces;
using SettlementManager.Domain.Settlements.Repositories;
using SettlementManager.Infrastructure.Persistence.Settlements.Repositories;

namespace SettlementManager.Infrastructure.Persistence.Database;

internal static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        const string sectionName = "Database";
        string connectionString = configuration.GetConnectionString(sectionName)!;

        services.AddDbContext<SettlementManagerDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddHostedService<DatabaseInitializer>();

        services.AddScoped<IUnitOfWork>(serviceProvider =>
                serviceProvider.GetRequiredService<SettlementManagerDbContext>()
        );

        services.AddScoped<ISettlementRepository, SettlementRepository>();

        return services;
    }
}