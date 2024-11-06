using ErrorOr;
using Microsoft.AspNetCore.Components;
using SettlementManager.Application.Settlements.DTO;
using SettlementManager.Infrastructure.Persistence.Settlements.Queries.GetSettlements;
using SettlementManager.Web.Models;

namespace SettlementManager.Web.Services.Settlements;

public interface ISettlementService
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string? Search { get; set; }
    public Action? OnDataChanged { get; set; }
    public SettlementPagedResponse? CurrentResponse { get; protected set; }
    public IReadOnlyList<CountryDto>? Countries { get; protected set; }
    public AddSettlementModel NewSettlement { get; protected set; }
    public bool IsAddSettlementModalOpen { get; protected set; }

    Task<SettlementPagedResponse?> GetSettlementsAsync(string? search, int pageNumber, int pageSize);
    Task SearchSettlements();
    Task NextPage();
    Task PreviousPage();
    Task LastPage();
    Task ChangePageSize(ChangeEventArgs changeEvent);

    Task<IReadOnlyList<CountryDto>?> GetCountriesAsync();
    Task OpenAddSettlementModal();
    void CloseAddSettlementModal();
    Task SaveSettlement();
}