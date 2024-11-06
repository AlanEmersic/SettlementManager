using ErrorOr;
using MediatR;
using SettlementManager.Application.Common.Interfaces;
using SettlementManager.Application.Settlements.Mappings;
using SettlementManager.Domain.Settlements;
using SettlementManager.Domain.Settlements.Errors;
using SettlementManager.Domain.Settlements.Repositories;

namespace SettlementManager.Application.Settlements.Commands.CreateSettlement;

internal sealed class CreateSettlementCommandHandler : IRequestHandler<CreateSettlementCommand, ErrorOr<Created>>
{
    private readonly ISettlementRepository settlementRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateSettlementCommandHandler(ISettlementRepository settlementRepository, IUnitOfWork unitOfWork)
    {
        this.settlementRepository = settlementRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Created>> Handle(CreateSettlementCommand command, CancellationToken cancellationToken)
    {
        Settlement settlement = command.MapToDomain();

        bool isSettlementAlreadyCreated = await settlementRepository.IsCreated(settlement);

        if (isSettlementAlreadyCreated)
        {
            return SettlementErrors.SettlementAlreadyCreated;
        }

        await settlementRepository.AddAsync(settlement);
        await unitOfWork.CommitChangesAsync(cancellationToken);

        return Result.Created;
    }
}