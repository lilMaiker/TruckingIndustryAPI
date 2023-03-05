using Microsoft.EntityFrameworkCore;
using TruckingIndustryAPI.Data;

namespace TruckingIndustryAPI.Repository.Status
{
    public class StatusRepository : GenericRepository<Entities.Models.Status>, IStatusRepository
    {
        public StatusRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Entities.Models.Status>> GetAllAsync()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(StatusRepository));
                return new List<Entities.Models.Status>();
            }
        }

        public override async Task<bool> UpdateAsync(Entities.Models.Status entity)
        {
            try
            {
                var existingentity = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

                if (existingentity == null)
                    return await AddAsync(entity);

                existingentity.NameStatus = entity.NameStatus;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(StatusRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(StatusRepository));
                return false;
            }
        }

    }
}
