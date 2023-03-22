using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.BidsFeatures.Commands
{
    public class UpdateBidCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }
        public long CarsId { get; set; }
        public long FoundationId { get; set; }
        public double FreightAMount { get; set; }
        public long CurrencyId { get; set; }
        public DateTime DateToLoad { get; set; }
        public DateTime DateToUnload { get; set; }
        public string ActAccNumber { get; set; }
        public long StatusId { get; set; }
        public DateTime PayDate { get; set; }
        public long EmployeeId { get; set; }
        public class UpdateBidsCommandHandler : IRequestHandler<UpdateBidCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdateBidsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ICommandResult> Handle(UpdateBidCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _unitOfWork.Bids.GetByIdAsync(command.Id);
                    if (result == null) return new NotFoundResult() { };
                    _mapper.Map(command, result);
                    await _unitOfWork.Bids.UpdateAsync(result);
                    await _unitOfWork.CompleteAsync();
                    return new CommandResult() { Data = result, Success = true };
                }
                catch (Exception ex)
                {
                    return new BadRequestResult() { Error = ex.Message };
                }
            }
        }
    }
}
