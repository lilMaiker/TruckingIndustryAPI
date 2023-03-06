using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Repository.Cargos
{
    public interface ICargoRepository : IGenericRepository<Cargo>
    {
        Task<IEnumerable<Cargo>> GetByIdBidAsync(long IdBid);
    }
}
