using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.CarsFeatures.Queries
{
    public class GetAllCarsQuery : IRequest<IEnumerable<Cars>>
    {
        public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, IEnumerable<Cars>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCarsQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Cars>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Cars.GetAllAsync();
            }
        }
    }
}
