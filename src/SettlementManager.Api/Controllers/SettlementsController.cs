using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SettlementManager.Infrastructure.Persistence.Settlements.Queries.GetSettlements;

namespace SettlementManager.Api.Controllers;

[Route("api/[controller]")]
public sealed class SettlementsController : ApiController
{
    private readonly ISender mediator;

    public SettlementsController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetSettlements(string? search, int pageNumber = 1, int pageSize = 10)
    {
        GetSettlementsQuery query = new(search, pageNumber, pageSize);
        ErrorOr<SettlementPagedResponse> result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }
}