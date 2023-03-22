using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Services
{
    public interface ICargoService
    {
        Task<bool> CanFitCargo(Car car, Cargo cargo);
        Task<bool> CanSetTypeCargo(Car car, Cargo cargo);
        Task<string> GetErrorMessage(Car car, Cargo cargo);
    }
}
