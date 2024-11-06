using Microsoft.EntityFrameworkCore;
using SettlementManager.Domain.Settlements;
using SettlementManager.Domain.Settlements.Repositories;
using SettlementManager.Infrastructure.Persistence.Database;

namespace SettlementManager.Infrastructure.Persistence.Settlements.Repositories;

internal sealed class SettlementRepository : ISettlementRepository
{
    private readonly SettlementManagerDbContext dbContext;

    public SettlementRepository(SettlementManagerDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Settlement?> GetByIdAsync(int id)
    {
        return await dbContext.Settlements
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> IsCreated(Settlement settlement)
    {
        return await dbContext.Settlements
            .AsNoTracking()
            .AnyAsync(x => x.Name == settlement.Name &&
                           x.PostalCode == settlement.PostalCode &&
                           x.CountryId == settlement.CountryId);
    }

    public async Task AddAsync(Settlement settlement)
    {
        dbContext.Settlements.Add(settlement);

        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Settlement settlement)
    {
        dbContext.Settlements.Update(settlement);

        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Settlement settlement)
    {
        dbContext.Settlements.Remove(settlement);

        await Task.CompletedTask;
    }
}