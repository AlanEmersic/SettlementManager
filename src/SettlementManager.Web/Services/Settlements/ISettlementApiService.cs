using SettlementManager.Application.Settlements.DTO;
using SettlementManager.Web.Models;

namespace SettlementManager.Web.Services.Settlements;

public interface ISettlementApiService
{
    Task<SettlementPagedDto?> GetSettlementsAsync(string? search, int pageNumber, int pageSize);
    Task<IReadOnlyList<CountryDto>?> GetCountriesAsync();
    Task<HttpResponseMessage> SaveSettlementAsync(AddSettlementModel settlementModel);
    Task<HttpResponseMessage> UpdateSettlementAsync(EditSettlementModel settlementModel);
    Task<HttpResponseMessage> DeleteSettlementAsync(int id);
}