using ErrorOr;
using MediatR;
using SettlementManager.Application.Common.Interfaces;
using SettlementManager.Domain.Settlements;
using SettlementManager.Domain.Settlements.Errors;
using SettlementManager.Domain.Settlements.Repositories;

namespace SettlementManager.Application.Settlements.Commands.DeleteSettlement;

internal sealed class DeleteSettlementCommandHandler : IRequestHandler<DeleteSettlementCommand, ErrorOr<Deleted>>
{
    private readonly ISettlementRepository settlementRepository;
    private readonly IUnitOfWork unitOfWork;

    public DeleteSettlementCommandHandler(ISettlementRepository settlementRepository, IUnitOfWork unitOfWork)
    {
        this.settlementRepository = settlementRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteSettlementCommand command, CancellationToken cancellationToken)
    {
        Settlement? settlement = await settlementRepository.GetByIdAsync(command.Id);

        if (settlement is null)
        {
            return SettlementErrors.SettlementNotFound;
        }

        await settlementRepository.DeleteAsync(settlement);
        await unitOfWork.CommitChangesAsync(cancellationToken);

        return Result.Deleted;
    }
}