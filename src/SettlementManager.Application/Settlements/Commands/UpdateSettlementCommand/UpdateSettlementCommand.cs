using ErrorOr;
using MediatR;

namespace SettlementManager.Application.Settlements.Commands.UpdateSettlementCommand;

public sealed record UpdateSettlementCommand(int Id, int CountryId, string Name, string PostalCode) : IRequest<ErrorOr<Updated>>;