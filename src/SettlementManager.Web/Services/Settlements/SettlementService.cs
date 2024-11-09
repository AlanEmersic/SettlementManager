using SettlementManager.Application.Settlements.DTO;
using SettlementManager.Web.Models;

namespace SettlementManager.Web.Services.Settlements;

internal sealed class SettlementService : ISettlementService
{
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; private set; } = 1;
    public string? Search { get; set; }

    public Action? OnDataChanged { get; set; } = delegate { };
    public SettlementPagedDto? CurrentResponse { get; set; }
    public IReadOnlyList<CountryDto>? Countries { get; set; } = new List<CountryDto>();
    public AddSettlementModel AddSettlement { get; set; } = new();
    public EditSettlementModel EditSettlement { get; set; } = null!;
    public SettlementDto SelectedSettlement { get; set; } = null!;
    public bool IsAddSettlementModalOpen { get; set; }
    public bool IsEditSettlementModalOpen { get; set; }
    public bool IsDeleteConfirmationModalOpen { get; set; }

    private readonly ISettlementApiService settlementApiService;

    public SettlementService(ISettlementApiService settlementApiService)
    {
        this.settlementApiService = settlementApiService;
    }

    public async Task SearchSettlements()
    {
        await LoadSettlements();
    }

    public async Task FirstPage()
    {
        PageNumber = 1;
        await LoadSettlements();
    }

    public async Task NextPage()
    {
        if (!CurrentResponse!.HasNextPage)
        {
            return;
        }

        PageNumber++;
        await LoadSettlements();
    }

    public async Task PreviousPage()
    {
        if (!CurrentResponse!.HasPreviousPage)
        {
            return;
        }

        PageNumber--;
        await LoadSettlements();
    }

    public async Task LastPage()
    {
        PageNumber = CurrentResponse!.PageCount;
        await LoadSettlements();
    }

    public async Task ChangePageSize(int pageSize)
    {
        PageNumber = 1;
        PageSize = pageSize;
        await LoadSettlements();
    }

    public async Task OpenAddSettlementModal()
    {
        IsAddSettlementModalOpen = true;

        if (Countries is null || Countries.Count == 0)
        {
            Countries = await settlementApiService.GetCountriesAsync();
        }
    }

    public void CloseAddSettlementModal()
    {
        IsAddSettlementModalOpen = false;
        AddSettlement = new AddSettlementModel();
    }

    public async Task SaveNewSettlement()
    {
        HttpResponseMessage responseMessage = await settlementApiService.SaveSettlementAsync(AddSettlement);

        if (responseMessage.IsSuccessStatusCode)
        {
            CloseAddSettlementModal();
            CurrentResponse = await settlementApiService.GetSettlementsAsync(Search, PageNumber, PageSize);
        }
    }

    public async Task OpenEditSettlementModal(SettlementDto settlement)
    {
        if (Countries is null || Countries.Count == 0)
        {
            Countries = await settlementApiService.GetCountriesAsync();
        }

        EditSettlement = new EditSettlementModel(settlement.Id, settlement.Name, settlement.PostalCode, settlement.Country.Id);
        IsEditSettlementModalOpen = true;
        OnDataChanged?.Invoke();
    }

    public void CloseEditSettlementModal()
    {
        IsEditSettlementModalOpen = false;
        OnDataChanged?.Invoke();
    }

    public async Task SaveEditedSettlement()
    {
        IsEditSettlementModalOpen = false;

        HttpResponseMessage responseMessage = await settlementApiService.UpdateSettlementAsync(EditSettlement);

        if (responseMessage.IsSuccessStatusCode)
        {
            CloseEditSettlementModal();
        }

        await LoadSettlements();
    }

    public void OpenDeleteConfirmationModal(SettlementDto settlement)
    {
        SelectedSettlement = settlement;
        IsDeleteConfirmationModalOpen = true;
        OnDataChanged?.Invoke();
    }

    public void CloseDeleteConfirmationModal()
    {
        IsDeleteConfirmationModalOpen = false;
        OnDataChanged?.Invoke();
    }

    public async Task DeleteSettlement()
    {
        IsDeleteConfirmationModalOpen = false;

        HttpResponseMessage responseMessage = await settlementApiService.DeleteSettlementAsync(SelectedSettlement.Id);

        if (responseMessage.IsSuccessStatusCode)
        {
            CloseDeleteConfirmationModal();
        }

        await LoadSettlements();
    }

    private async Task LoadSettlements()
    {
        CurrentResponse = await settlementApiService.GetSettlementsAsync(Search, PageNumber, PageSize);
        OnDataChanged?.Invoke();
    }
}