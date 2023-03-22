using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.BidsFeatures.Commands
{
    public class DeleteBidCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public class DeleteBidsCommandHandler : IRequestHandler<DeleteBidCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteBidsCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<ICommandResult> Handle(DeleteBidCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Bids.GetByIdAsync(command.Id);
                    if (result == null) return new NotFoundResult() { Data = nameof(Bid) };
                    await _unitOfWork.Bids.DeleteAsync(result.Id);
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
