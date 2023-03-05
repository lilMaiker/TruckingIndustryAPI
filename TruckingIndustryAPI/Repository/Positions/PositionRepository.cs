using Microsoft.EntityFrameworkCore;

using TruckingIndustryAPI.Data;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Extensions.Attributes;

namespace TruckingIndustryAPI.Repository.Positions
{
    
    public class PositionRepository : GenericRepository<Position>, IPositionRepository
    {
        public PositionRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Position>> GetAllAsync()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(PositionRepository));
                return new List<Position>();
            }
        }

        public override async Task<bool> UpdateAsync(Position entity)
        {
            try
            {
                var existingentity = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

                if (existingentity == null)
                    return await AddAsync(entity);

                existingentity.NamePosition = entity.NamePosition;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(PositionRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(PositionRepository));
                return false;
            }
        }
    }
}
