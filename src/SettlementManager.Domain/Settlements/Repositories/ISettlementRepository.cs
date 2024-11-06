namespace SettlementManager.Domain.Settlements.Repositories;

public interface ISettlementRepository
{
    Task<Settlement?> GetByIdAsync(int id);
    Task<bool> IsCreated(Settlement settlement);
    Task AddAsync(Settlement settlement);
    Task UpdateAsync(Settlement settlement);
    Task DeleteAsync(Settlement settlement);
}