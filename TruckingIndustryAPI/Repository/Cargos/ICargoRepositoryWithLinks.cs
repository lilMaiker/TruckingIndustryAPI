using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Repository.Cargos
{
    public interface ICargoRepositoryWithLinks : IGenericRepository<Cargo>
    {
        /// <summary>
        /// Asynchronously retrieves a collection of Cargo objects associated with the specified IdBid.
        /// </summary>
        Task<IEnumerable<Cargo>> GetByIdBidAsync(long IdBid);

        /// <summary>
        /// Asynchronously retrieves the total weight of a car by its ID.
        /// </summary>
        /// <param name="carId">The ID of the car.</param>
        /// <returns>A Task containing the total weight of the car.</returns>
        Task<double> GetTotalWeightByCarIdAsync(long carId);
    }
}
