using Microsoft.EntityFrameworkCore;

using TruckingIndustryAPI.Data;

namespace TruckingIndustryAPI.Repository.Cars
{
    public class CarsRepository : GenericRepository<Entities.Models.Car>, ICarsRepository
    {
        public CarsRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Entities.Models.Car>> GetAllAsync()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(CarsRepository));
                return new List<Entities.Models.Car>();
            }
        }

        public override async Task<bool> UpdateAsync(Entities.Models.Car entity)
        {
            try
            {
                var existingentity = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

                if (existingentity == null)
                    return await AddAsync(entity);

                existingentity.BrandTrailer = entity.BrandTrailer;
                existingentity.TrailerNumber = entity.TrailerNumber;
                existingentity.LastDateTechnicalInspection = entity.LastDateTechnicalInspection;
                existingentity.MaxWeight = entity.MaxWeight;
                existingentity.WithOpenSide = entity.WithOpenSide;
                existingentity.WithRefrigerator = entity.WithRefrigerator;
                existingentity.WithTent = entity.WithTent;
                existingentity.WithHydroboard = entity.WithHydroboard;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(CarsRepository));
                return false;
            }
        }

        public override async Task<bool> DeleteAsync(long id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(CarsRepository));
                return false;
            }
        }

    }
}
