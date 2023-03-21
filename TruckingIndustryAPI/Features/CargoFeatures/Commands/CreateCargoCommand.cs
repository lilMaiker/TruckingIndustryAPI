using AutoMapper;

using MediatR;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Command;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Features.CargoFeatures.Commands
{
    public class CreateCargoCommand : IRequest<ICommandResult>
    {
        public string NameCargo { get; set; }
        public double WeightCargo { get; set; }
        public long TypeCargoId { get; set; }
        public long BidsId { get; set; }
        public class CreateCargoCommandHandler : IRequestHandler<CreateCargoCommand, ICommandResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreateCargoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ICommandResult> Handle(CreateCargoCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    // Преобразуем команду в сущность груза
                    var cargo = _mapper.Map<Cargo>(command);

                    // Получаем заявку и трансопрт, связанные с грузом
                    var bid = await _unitOfWork.Bids.GetByIdAsync(cargo.BidsId);
                    var car = await _unitOfWork.Cars.GetByIdAsync(bid.CarsId);

                    // Проверяем, может ли трансопрт вместить груз
                    if (!CanFitCargo(car, cargo).Result) return new BadRequestResult() { Errors = GetErrorMessage(car, cargo).Result };

                    // Добавляем груз и сохраняем изменения
                    await _unitOfWork.Cargo.AddAsync(cargo);
                    await _unitOfWork.CompleteAsync();

                    return new CommandResult() { Data = cargo, Success = true };
                }
                catch (Exception ex)
                {
                    return new BadRequestResult() { Errors = ex.Message };
                }
            }

            /// <summary>
            /// Вспомогательный метод для проверки, поместится ли груз в автомобиль
            /// </summary>
            /// <param name="car"></param>
            /// <param name="cargo"></param>
            /// <returns></returns>
            private async Task<bool> CanFitCargo(Car car, Cargo cargo)
            {
                // Получаем общее пространство и вес автомобиля
                double maxWeightCar = car.MaxWeight;

                // Получаем общий вес грузов в автомобиле
                double sumWeight = await _unitOfWork.Cargo.GetTotalWeightByCarIdAsync(car.Id);

                // Получаем вес груза, который нужно добавить
                var weightCargo = cargo.WeightCargo;

                // Возвращаем true, если есть достаточно места для груза, false в противном случае
                return maxWeightCar >= sumWeight + weightCargo;
            }

            /// <summary>
            /// Вспомогательный метод для получения сообщения об ошибке, когда груз не помещается в автомобиль
            /// </summary>
            /// <param name="car"></param>
            /// <param name="cargo"></param>
            /// <returns></returns>
            private async Task<string> GetErrorMessage(Car car, Cargo cargo)
            {
                // Получаем общее пространство и вес автомобиля
                double maxWeightCar = car.MaxWeight;

                // Получаем общий вес грузов в автомобиле
                double sumWeight = await _unitOfWork.Cargo.GetTotalWeightByCarIdAsync(car.Id);

                // Проверяем, если

                // Автомобиль полон или перегружен
                if (maxWeightCar <= sumWeight) return "Автомобиль полон или перегружен.";

                // Автомобиль не имеет достаточно места для определенного количества кг
                else
                {
                    var freeWeight = maxWeightCar - sumWeight;
                    var weightCargo = cargo.WeightCargo;
                    return $"Автомобиль не имеет достаточно места для {weightCargo} кг. Осталось только {freeWeight} кг.";
                }
            }
        }
    }
}
