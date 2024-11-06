using SettlementManager.Application.Settlements.DTO;
using SettlementManager.Application.Settlements.Requests;
using SettlementManager.Infrastructure.Persistence.Settlements.Queries.GetSettlements;
using SettlementManager.Web.Models;

namespace SettlementManager.Web.Services.Settlements;

public sealed class SettlementService : ISettlementService
{
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
    public string? Search { get; set; }

    public Action? OnDataChanged { get; set; } = delegate { };
    public SettlementPagedResponse? CurrentResponse { get; set; }
    public IReadOnlyList<CountryDto>? Countries { get; set; } = new List<CountryDto>();
    public AddSettlementModel NewSettlement { get; set; } = new();
    public EditSettlementModel EditSettlement { get; set; } = null!;
    public SettlementDto SelectedSettlement { get; set; } = null!;
    public bool IsAddSettlementModalOpen { get; set; }
    public bool IsEditSettlementModalOpen { get; set; }
    public bool IsDeleteConfirmationModalOpen { get; set; }

    private readonly HttpClient httpClient;
    private readonly ILogger<SettlementService> logger;

    public SettlementService(HttpClient httpClient, ILogger<SettlementService> logger)
    {
        this.httpClient = httpClient;
        this.logger = logger;
    }

    public async Task<SettlementPagedResponse?> GetSettlementsAsync(string? search, int pageNumber, int pageSize)
    {
        try
        {
            string requestUri = $"api/Settlements?search={search}&pageNumber={pageNumber}&pageSize={pageSize}";

            SettlementPagedResponse? response = await httpClient.GetFromJsonAsync<SettlementPagedResponse?>(requestUri);
            CurrentResponse = response;

            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return null;
        }
    }

    public async Task<IReadOnlyList<CountryDto>?> GetCountriesAsync()
    {
        try
        {
            string requestUri = "api/Countries";

            IReadOnlyList<CountryDto>? response = await httpClient.GetFromJsonAsync<IReadOnlyList<CountryDto>?>(requestUri);
            Countries = response;

            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return null;
        }
    }

    public async Task SaveSettlementAsync()
    {
        try
        {
            string requestUri = "api/Settlements";
            CreateSettlementRequest request = new(NewSettlement.CountryId, NewSettlement.Name, NewSettlement.PostalCode);

            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(requestUri, request);

            if (responseMessage.IsSuccessStatusCode)
            {
                CloseAddSettlementModal();
                await GetSettlementsAsync(Search, PageNumber, PageSize);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }
    }

    private async Task UpdateSettlementAsync()
    {
        try
        {
            string requestUri = "api/Settlements";
            UpdateSettlementRequest request = new(EditSettlement.Id, EditSettlement.CountryId, EditSettlement.Name, EditSettlement.PostalCode);

            HttpResponseMessage responseMessage = await httpClient.PutAsJsonAsync(requestUri, request);

            if (responseMessage.IsSuccessStatusCode)
            {
                CloseEditSettlementModal();
            }

            await GetSettlementsAsync(Search, PageNumber, PageSize);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }
    }

    private async Task DeleteSettlementAsync(int id)
    {
        try
        {
            string requestUri = $"api/Settlements?id={id}";

            HttpResponseMessage responseMessage = await httpClient.DeleteAsync(requestUri);

            if (responseMessage.IsSuccessStatusCode)
            {
                CloseDeleteConfirmationModal();
            }

            await GetSettlementsAsync(Search, PageNumber, PageSize);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }
    }

    public async Task SearchSettlements()
    {
        PageNumber = 1;

        await GetSettlementsAsync(Search, PageNumber, PageSize);
        OnDataChanged?.Invoke();
    }

    public async Task NextPage()
    {
        if (!CurrentResponse!.HasNextPage)
        {
            return;
        }

        PageNumber++;

        await GetSettlementsAsync(Search, PageNumber, PageSize);
        OnDataChanged?.Invoke();
    }

    public async Task PreviousPage()
    {
        if (!CurrentResponse!.HasPreviousPage)
        {
            return;
        }

        PageNumber--;

        await GetSettlementsAsync(Search, PageNumber, PageSize);
        OnDataChanged?.Invoke();
    }

    public async Task LastPage()
    {
        PageNumber = CurrentResponse!.PageCount;

        await GetSettlementsAsync(Search, PageNumber, PageSize);
        OnDataChanged?.Invoke();
    }

    public async Task ChangePageSize(int pageSize)
    {
        PageNumber = 1;
        PageSize = pageSize;

        await GetSettlementsAsync(Search, PageNumber, PageSize);
        OnDataChanged?.Invoke();
    }

    public async Task OpenAddSettlementModal()
    {
        IsAddSettlementModalOpen = true;

        await GetCountriesAsync();
    }

    public void CloseAddSettlementModal()
    {
        IsAddSettlementModalOpen = false;
        NewSettlement = new AddSettlementModel();
    }

    public async Task OpenEditSettlementModal(SettlementDto settlement)
    {
        if (Countries is null || Countries.Count == 0)
        {
            Countries = await GetCountriesAsync();
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

        await UpdateSettlementAsync();
        OnDataChanged?.Invoke();
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

        await DeleteSettlementAsync(SelectedSettlement.Id);
        OnDataChanged?.Invoke();
    }
}