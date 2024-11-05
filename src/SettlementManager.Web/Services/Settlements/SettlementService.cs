﻿using ErrorOr;
using Microsoft.AspNetCore.Components;
using SettlementManager.Infrastructure.Persistence.Settlements.Queries.GetSettlements;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace SettlementManager.Web.Services.Settlements;

public sealed class SettlementService : ISettlementService
{
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
    public string? Search { get; set; }

    public Action? OnDataChanged { get; set; } = delegate { };
    public SettlementPagedResponse? CurrentResponse { get; set; }

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

    public async Task ChangePageSize(ChangeEventArgs changeEvent)
    {
        PageNumber = 1;
        PageSize = int.Parse(changeEvent.Value!.ToString()!);

        await GetSettlementsAsync(Search, PageNumber, PageSize);
        OnDataChanged?.Invoke();
    }
}