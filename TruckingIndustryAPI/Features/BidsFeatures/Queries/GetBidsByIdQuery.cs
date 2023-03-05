using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Exceptions;

namespace TruckingIndustryAPI.Features.BidsFeatures.Queries
{
    public class GetBidsByIdQuery : IRequest<Bids>
    {
        public long Id { get; set; }
        public class GetBidsByIdQueryHandler : IRequestHandler<GetBidsByIdQuery, Bids>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetBidsByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Bids> Handle(GetBidsByIdQuery request, CancellationToken cancellationToken)
            {
                var result = await _unitOfWork.Bids.GetByIdAsync(request.Id);
                if (result == null) throw new NotFoundException(nameof(Bids));
                return result;
            }
        }
    }
}
