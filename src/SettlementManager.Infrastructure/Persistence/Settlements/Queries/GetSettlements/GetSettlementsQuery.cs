using ErrorOr;
using MediatR;

namespace SettlementManager.Infrastructure.Persistence.Settlements.Queries.GetSettlements;

public sealed record GetSettlementsQuery(string? Search, int PageNumber, int PageSize) : IRequest<ErrorOr<SettlementPagedResponse>>;