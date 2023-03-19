using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.BidsFeatures.Commands
{
    public class UpdateBidsCommand : IRequest<Bid>
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
        public class UpdateBidsCommandHandler : IRequestHandler<UpdateBidsCommand, Bid>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public UpdateBidsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Bid> Handle(UpdateBidsCommand command, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Bids.GetByIdAsync(command.Id);
                if (result == null) throw new NotFoundException(nameof(Bid));
                _mapper.Map(command, result);
                await _unitOfWork.Bids.UpdateAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
