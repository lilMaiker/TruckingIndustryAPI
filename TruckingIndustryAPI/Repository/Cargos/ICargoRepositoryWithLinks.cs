using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Repository.Cargos
{
    public interface ICargoRepositoryWithLinks : IGenericRepository<Cargo>
    {
        Task<IEnumerable<Cargo>> GetByIdBidAsync(long IdBid);
        Task<double> GetTotalWeightByCarIdAsync(long carId);
    }
}
