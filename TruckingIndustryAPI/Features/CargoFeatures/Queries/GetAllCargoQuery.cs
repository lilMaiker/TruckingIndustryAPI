using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.CargoFeatures.Queries
{
    public class GetAllCargoQuery : IRequest<IEnumerable<Cargo>>
    {
        public class GetAllCargoQueryHandler : IRequestHandler<GetAllCargoQuery, IEnumerable<Cargo>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCargoQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Cargo>> Handle(GetAllCargoQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Cargo.GetAllAsync();
            }
        }
    }
}
