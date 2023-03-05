using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.TypeCargoFeatures.Queries
{
    public class GetAllTypeCargoQuery : IRequest<IEnumerable<TypeCargo>>
    {
        public class GetAllTypeCargoQueryHandler : IRequestHandler<GetAllTypeCargoQuery, IEnumerable<TypeCargo>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllTypeCargoQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<TypeCargo>> Handle(GetAllTypeCargoQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.TypeCargo.GetAllAsync();
            }
        }
    }
}
