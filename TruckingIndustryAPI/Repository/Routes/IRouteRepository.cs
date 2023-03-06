using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Repository.Routes
{
    public interface IRouteRepository : IGenericRepository<Entities.Models.Route>
    {
        Task<IEnumerable<Entities.Models.Route>> GetByIdBidAsync(long IdBid);
    }
}
