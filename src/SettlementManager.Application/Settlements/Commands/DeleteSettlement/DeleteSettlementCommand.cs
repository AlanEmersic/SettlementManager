using ErrorOr;
using MediatR;

namespace SettlementManager.Application.Settlements.Commands.DeleteSettlement;

public sealed record DeleteSettlementCommand(int Id) : IRequest<ErrorOr<Deleted>>;