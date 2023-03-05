using Microsoft.EntityFrameworkCore;
using TruckingIndustryAPI.Data;

namespace TruckingIndustryAPI.Repository.Routes
{
    public class RouteRepository : GenericRepository<Entities.Models.Route>, IRouteRepository
    {
        public RouteRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Entities.Models.Route>> GetAllAsync()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(RouteRepository));
                return new List<Entities.Models.Route>();
            }
        }

        public override async Task<bool> UpdateAsync(Entities.Models.Route entity)
        {
            try
            {
                var existingentity = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

                if (existingentity == null)
                    return await AddAsync(entity);

                existingentity.PointA = entity.PointA;

                existingentity.PointB = entity.PointB;

                existingentity.BidsId = entity.BidsId;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(RouteRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(RouteRepository));
                return false;
            }
        }

    }
}
