using ErrorOr;
using MediatR;
using SettlementManager.Application.Settlements.DTO;

namespace SettlementManager.Infrastructure.Persistence.Settlements.Queries.GetSettlements;

public sealed record GetSettlementsQuery(string? Search, int PageNumber, int PageSize) : IRequest<ErrorOr<SettlementPagedDto>>;