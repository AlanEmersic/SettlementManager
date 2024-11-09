using SettlementManager.Application.Settlements.DTO;
using SettlementManager.Web.Models;

namespace SettlementManager.Web.Services.Settlements;

public interface ISettlementService
{
    public int PageSize { get; set; }
    public string? Search { get; set; }
    public Action? OnDataChanged { get; set; }
    public SettlementPagedDto? CurrentResponse { get; protected set; }
    public IReadOnlyList<CountryDto>? Countries { get; protected set; }
    public AddSettlementModel AddSettlement { get; protected set; }
    public EditSettlementModel EditSettlement { get; protected set; }
    public SettlementDto SelectedSettlement { get; protected set; }
    public bool IsAddSettlementModalOpen { get; protected set; }
    public bool IsEditSettlementModalOpen { get; protected set; }
    public bool IsDeleteConfirmationModalOpen { get; protected set; }

    Task SearchSettlements();
    Task FirstPage();
    Task NextPage();
    Task PreviousPage();
    Task LastPage();
    Task ChangePageSize(int pageSize);

    Task OpenAddSettlementModal();
    void CloseAddSettlementModal();
    Task SaveNewSettlement();

    Task OpenEditSettlementModal(SettlementDto settlement);
    void CloseEditSettlementModal();
    Task SaveEditedSettlement();

    void OpenDeleteConfirmationModal(SettlementDto settlement);
    void CloseDeleteConfirmationModal();
    Task DeleteSettlement();
}