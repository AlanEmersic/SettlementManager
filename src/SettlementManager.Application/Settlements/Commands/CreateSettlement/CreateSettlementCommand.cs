using ErrorOr;
using MediatR;

namespace SettlementManager.Application.Settlements.Commands.CreateSettlement;

public sealed record CreateSettlementCommand(int CountryId, string Name, string PostalCode) : IRequest<ErrorOr<Created>>;