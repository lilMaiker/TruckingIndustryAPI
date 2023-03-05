using MediatR;
using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.BidsFeatures.Queries
{
    public class GetAllBidsQuery : IRequest<IEnumerable<Bids>>
    {
        public class GetAllBidsQueryHandler : IRequestHandler<GetAllBidsQuery, IEnumerable<Bids>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllBidsQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Bids>> Handle(GetAllBidsQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Bids.GetAllAsync();
            }
        }
    }
}
