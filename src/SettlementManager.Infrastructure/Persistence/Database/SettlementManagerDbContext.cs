using Microsoft.EntityFrameworkCore;
using SettlementManager.Application.Common.Interfaces;
using SettlementManager.Domain.Settlements;

namespace SettlementManager.Infrastructure.Persistence.Database;

internal sealed class SettlementManagerDbContext : DbContext, IUnitOfWork
{
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Settlement> Settlements => Set<Settlement>();

    public SettlementManagerDbContext(DbContextOptions<SettlementManagerDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SettlementManagerDbContext).Assembly);
    }

    public async Task CommitChangesAsync(CancellationToken cancellationToken = default)
    {
        await SaveChangesAsync(cancellationToken);
    }
}