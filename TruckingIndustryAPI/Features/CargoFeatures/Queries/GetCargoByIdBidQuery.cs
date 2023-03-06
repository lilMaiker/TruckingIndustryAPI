using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.CargoFeatures.Queries
{
    public class GetCargoByIdBidQuery : IRequest<IEnumerable<Cargo>>
    {
        public long Id { get; set; }
        public class GetCargoByIdBidQueryHandler : IRequestHandler<GetCargoByIdBidQuery, IEnumerable<Cargo>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetCargoByIdBidQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Cargo>> Handle(GetCargoByIdBidQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Cargo.GetByIdBidAsync(request.Id);
            }
        }
    }
}
