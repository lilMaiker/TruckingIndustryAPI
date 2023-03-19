using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.CargoFeatures.Commands
{
    public class CreateCargoCommand : IRequest<Cargo>
    {
        public string NameCargo { get; set; }
        public double WeightCargo { get; set; }
        public long TypeCargoId { get; set; }
        public long BidsId { get; set; }
        public class CreateCargoCommandHandler : IRequestHandler<CreateCargoCommand, Cargo>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreateCargoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Cargo> Handle(CreateCargoCommand command, CancellationToken cancellationToken)
            {
                var result = _mapper.Map<Cargo>(command);

                var bid = await _unitOfWork.Bids.GetByIdAsync(result.BidsId);

                var car = await _unitOfWork.Cars.GetByIdAsync(bid.CarsId);

                //Получить всего места в машине
                double maxWeightCar = car.MaxWeight;

                var bids = await _unitOfWork.Bids.GetAllAsync();

                var bidForCar = bids.Where(w => w.CarsId == car.Id).ToList();

                double sumWeight = 0;

                foreach (var b in bidForCar)
                {
                   var cargo =  await _unitOfWork.Cargo.GetByIdBidAsync(b.Id);
                   sumWeight += cargo.Sum(s => s.WeightCargo);
                }

                if (maxWeightCar < sumWeight) throw new Exception("Транспортное средство переполнено.");

                if (maxWeightCar == sumWeight) throw new Exception("Транспортное средство заполнено полностью.");

                //Получить количества места которое можно добавить
                var freeWeight = maxWeightCar - sumWeight;

                //Получить количесвто которое я хочу добавить
                var weightCargo = result.WeightCargo;

                var sumAll = sumWeight + weightCargo;

                if (maxWeightCar < sumAll) throw new Exception($"В трансопрте недостаточно места для {weightCargo} кг. Осталось свободного {freeWeight}");

                await _unitOfWork.Cargo.AddAsync(result);
                await _unitOfWork.CompleteAsync();
                return result;
            }
        }
    }
}
