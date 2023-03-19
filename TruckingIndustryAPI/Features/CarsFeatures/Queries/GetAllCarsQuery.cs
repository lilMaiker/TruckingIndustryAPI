using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.CarsFeatures.Queries
{
    public class GetAllCarsQuery : IRequest<IEnumerable<Car>>
    {
        public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, IEnumerable<Car>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllCarsQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Car>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Cars.GetAllAsync();
            }
        }
    }
}
