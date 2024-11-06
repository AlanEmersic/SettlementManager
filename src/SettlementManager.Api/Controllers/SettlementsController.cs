using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SettlementManager.Application.Settlements.Commands.CreateSettlement;
using SettlementManager.Application.Settlements.Mappings;
using SettlementManager.Application.Settlements.Requests;
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

    [HttpPost]
    public async Task<IActionResult> CreateSettlement([FromBody] CreateSettlementRequest request)
    {
        CreateSettlementCommand command = request.MapToCommand();
        ErrorOr<Created> result = await mediator.Send(command);

        return result.Match(_ => CreatedAtAction(nameof(CreateSettlement), default), Problem);
    }
}