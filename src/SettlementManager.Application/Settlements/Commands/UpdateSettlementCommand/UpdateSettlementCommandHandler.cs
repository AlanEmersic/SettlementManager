using ErrorOr;
using MediatR;
using SettlementManager.Application.Common.Interfaces;
using SettlementManager.Application.Settlements.Mappings;
using SettlementManager.Domain.Settlements;
using SettlementManager.Domain.Settlements.Errors;
using SettlementManager.Domain.Settlements.Repositories;

namespace SettlementManager.Application.Settlements.Commands.UpdateSettlementCommand;

internal sealed class UpdateSettlementCommandHandler : IRequestHandler<UpdateSettlementCommand, ErrorOr<Updated>>
{
    private readonly ISettlementRepository settlementRepository;
    private readonly IUnitOfWork unitOfWork;

    public UpdateSettlementCommandHandler(ISettlementRepository settlementRepository, IUnitOfWork unitOfWork)
    {
        this.settlementRepository = settlementRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Updated>> Handle(UpdateSettlementCommand command, CancellationToken cancellationToken)
    {
        Settlement settlement = command.MapToDomain();

        Settlement? currentSettlement = await settlementRepository.GetByIdAsync(settlement.Id);

        if (currentSettlement is null)
        {
            return SettlementErrors.SettlementNotFound;
        }

        currentSettlement = settlement.MapToDomain(currentSettlement);

        await settlementRepository.UpdateAsync(currentSettlement);
        await unitOfWork.CommitChangesAsync(cancellationToken);

        return Result.Updated;
    }
}