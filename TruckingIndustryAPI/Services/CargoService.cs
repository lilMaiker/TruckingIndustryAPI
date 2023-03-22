using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Services
{
    public class CargoService : ICargoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CargoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Вспомогательный метод для проверки, поместится ли груз в автомобиль
        /// </summary>
        /// <param name="car"></param>
        /// <param name="cargo"></param>
        /// <returns></returns>
        public async Task<bool> CanFitCargo(Car car, Cargo cargo)
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
        /// Вспомогательный метод для проверки, можно ли такой тип груза доставлять в трнаспорте
        /// </summary>
        /// <param name="car"></param>
        /// <param name="cargo"></param>
        /// <returns></returns>
        public Task<bool> CanSetTypeCargo(Car car, Cargo cargo)
        {
            if ((cargo.TypeCargo.NameTypeCargo.Contains("Продукты питания") || cargo.TypeCargo.NameTypeCargo.Contains("Скоропортящийся")) && !car.WithRefrigerator)
                return Task.FromResult(false);
            else
                return Task.FromResult(true);
        }

        /// <summary>
        /// Вспомогательный метод для получения сообщения об ошибке, когда груз не помещается в автомобиль
        /// </summary>
        /// <param name="car"></param>
        /// <param name="cargo"></param>
        /// <returns></returns>
        public async Task<string> GetErrorMessage(Car car, Cargo cargo)
        {
            if ((cargo.TypeCargo.NameTypeCargo.Contains("Продукты питания") || cargo.TypeCargo.NameTypeCargo.Contains("Скоропортящийся")) && !car.WithRefrigerator)
                return $"В транспорте {car.TrailerNumber} отсутствует холодильник для доставки типа груза {cargo.TypeCargo.NameTypeCargo}.";

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
