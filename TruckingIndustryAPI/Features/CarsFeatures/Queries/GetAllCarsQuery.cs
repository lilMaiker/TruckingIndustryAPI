using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TruckingIndustryAPI.Features.CarsFeatures.Queries
{
    public class GetAllCarsQuery : IRequest<IEnumerable<CarWithFreeWeight>>
    {
        public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, IEnumerable<CarWithFreeWeight>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetAllCarsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<IEnumerable<CarWithFreeWeight>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
            {
               
                var result = await _unitOfWork.Cars.GetAllAsync();
                var carsWithFreeWeight = _mapper.Map<IEnumerable<CarWithFreeWeight>>(result);

                var cargo = await _unitOfWork.Cargo.GetAllAsync();
                var bids = await _unitOfWork.Bids.GetAllAsync();

                var query = from car in carsWithFreeWeight
                            join bid in bids on car.Id equals bid.CarsId
                            join c in cargo on bid.Id equals c.BidsId
                            group c by car into g
                            select new
                            {
                                Car = g.Key,
                                Cargo = g.ToList()
                            };

                foreach (var item in query)
                {
                    item.Car.LoadedWeight = item.Cargo.Sum(x => x.WeightCargo);
                }

                return carsWithFreeWeight;
            }
        }
    }
}
