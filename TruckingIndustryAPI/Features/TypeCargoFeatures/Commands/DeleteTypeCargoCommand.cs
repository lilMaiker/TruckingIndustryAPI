using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.TypeCargoFeatures.Commands
{
    public class DeleteTypeCargoCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public class DeleteTypeCargoCommandHandler : IRequestHandler<DeleteTypeCargoCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            public DeleteTypeCargoCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<ICommandResult> Handle(DeleteTypeCargoCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.TypeCargo.GetByIdAsync(command.Id);
                    if (result == null) return new NotFoundResult() { Data = nameof(TypeCargo) };
                    await _unitOfWork.TypeCargo.DeleteAsync(result.Id);
                    await _unitOfWork.CompleteAsync();
                    return new CommandResult() { Data = result.Id, Success = true };
                }
                catch (Exception ex)
                {
                    return new BadRequestResult() { Error = ex.Message };
                }
            }
        }
    }
}
