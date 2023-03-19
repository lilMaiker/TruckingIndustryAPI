using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.BidsFeatures.Commands
{
    public class CreateBidsCommand : IRequest<Bid>
    {
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
        public class CreateBidsCommandHandler : IRequestHandler<CreateBidsCommand, Bid>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreateBidsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Bid> Handle(CreateBidsCommand command, CancellationToken cancellationToken)
            {
                var result = _mapper.Map<Bid>(command);
                await _unitOfWork.Bids.AddAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
