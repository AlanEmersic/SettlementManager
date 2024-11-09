using SettlementManager.Application.Settlements.DTO;
using SettlementManager.Application.Settlements.Requests;
using SettlementManager.Web.Models;
using System.Net;

namespace SettlementManager.Web.Services.Settlements;

internal sealed class SettlementApiService : ISettlementApiService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ILogger<SettlementApiService> logger;

    private const string ClientName = "SettlementManagerApi";

    public SettlementApiService(IHttpClientFactory httpClientFactory, ILogger<SettlementApiService> logger)
    {
        this.httpClientFactory = httpClientFactory;
        this.logger = logger;
    }

    public async Task<SettlementPagedDto?> GetSettlementsAsync(string? search, int pageNumber, int pageSize)
    {
        try
        {
            string requestUri = $"api/Settlements?search={search}&pageNumber={pageNumber}&pageSize={pageSize}";
            HttpClient httpClient = httpClientFactory.CreateClient(ClientName);
            SettlementPagedDto? response = await httpClient.GetFromJsonAsync<SettlementPagedDto?>(requestUri);

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
            const string requestUri = "api/Countries";
            HttpClient httpClient = httpClientFactory.CreateClient(ClientName);
            IReadOnlyList<CountryDto>? response = await httpClient.GetFromJsonAsync<IReadOnlyList<CountryDto>?>(requestUri);

            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            return null;
        }
    }

    public async Task<HttpResponseMessage> SaveSettlementAsync(AddSettlementModel settlementModel)
    {
        try
        {
            const string requestUri = "api/Settlements";
            CreateSettlementRequest request = new(settlementModel.CountryId, settlementModel.Name, settlementModel.PostalCode);
            HttpClient httpClient = httpClientFactory.CreateClient(ClientName);
            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(requestUri, request);

            return responseMessage;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }

        return new HttpResponseMessage(HttpStatusCode.BadRequest);
    }

    public async Task<HttpResponseMessage> UpdateSettlementAsync(EditSettlementModel settlementModel)
    {
        try
        {
            const string requestUri = "api/Settlements";
            UpdateSettlementRequest request = new(settlementModel.Id, settlementModel.CountryId, settlementModel.Name, settlementModel.PostalCode);
            HttpClient httpClient = httpClientFactory.CreateClient(ClientName);
            HttpResponseMessage responseMessage = await httpClient.PutAsJsonAsync(requestUri, request);

            return responseMessage;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }

        return new HttpResponseMessage(HttpStatusCode.BadRequest);
    }

    public async Task<HttpResponseMessage> DeleteSettlementAsync(int id)
    {
        try
        {
            string requestUri = $"api/Settlements?id={id}";
            HttpClient httpClient = httpClientFactory.CreateClient(ClientName);
            HttpResponseMessage responseMessage = await httpClient.DeleteAsync(requestUri);

            return responseMessage;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }

        return new HttpResponseMessage(HttpStatusCode.BadRequest);
    }
}