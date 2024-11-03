namespace SettlementManager.Domain.Settlements.Repositories;

public interface ICountryRepository
{
    Task<Country?> GetByIdAsync(int id);
    Task AddAsync(Country country);
    Task UpdateAsync(Country country);
    Task DeleteAsync(Country country);
}