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
        //This method is an asynchronous task that returns a string. It is used to get an error message when a car does not have a refrigerator for delivering certain types
        // of cargo, or when the car is full or overloaded. It first checks if the cargo type contains "Food" or "Perishable" and if the car does not have a refrigerator. If
        // so, it returns an error message. Otherwise, it gets the total weight of the car and the total weight of the cargo in the car. It then checks if the car is full or
        // overloaded, and if so, it returns an error message. Otherwise, it calculates the free weight in the car and the weight of the cargo, and returns an error message
        // indicating that the car does not have enough space for the cargo.

        public async Task<string> GetErrorMessage(Car car, Cargo cargo)
        {
            if ((cargo.TypeCargo.NameTypeCargo.Contains("Продукты питания") || cargo.TypeCargo.NameTypeCargo.Contains("Скоропортящийся")) && !car.WithRefrigerator)
                return $"В транспорте {car.TrailerNumber} отсутствует холодильник для доставки типа груза {cargo.TypeCargo.NameTypeCargo}.";

            // Получаем общее пространство и вес автомобиля
            double maxWeightCar = car.MaxWeight;

            // Получаем общий вес грузов в автомобиле
            double sumWeight = await _unitOfWork.Cargo.GetTotalWeightByCarIdAsync(car.Id);

            // Автомобиль полон или перегружен
            if (maxWeightCar <= sumWeight) return "Автомобиль полон или перегружен.";

            // Автомобиль не имеет достаточно места для определенного количества кг.
            else
            {
                var freeWeight = maxWeightCar - sumWeight;
                var weightCargo = cargo.WeightCargo;
                return $"Автомобиль не имеет достаточно места для {weightCargo} кг. Осталось только {freeWeight} кг.";
            }
        }
    }
}
