using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SettlementManager.Application.Settlements.DTO;
using SettlementManager.Infrastructure.Persistence.Settlements.Queries.GetCountries;

namespace SettlementManager.Api.Controllers;

[Route("api/[controller]")]
public sealed class CountriesController : ApiController
{
    private readonly ISender mediator;

    public CountriesController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCountries()
    {
        GetCountriesQuery query = new();
        ErrorOr<IReadOnlyList<CountryDto>> result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }
}