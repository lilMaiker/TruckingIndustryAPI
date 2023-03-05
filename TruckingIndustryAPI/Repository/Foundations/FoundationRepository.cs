using Microsoft.EntityFrameworkCore;

using TruckingIndustryAPI.Data;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Repository.Foundations
{
    public class FoundationRepository : GenericRepository<Foundation>, IFoundationRepository
    {
        public FoundationRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Foundation>> GetAllAsync()
        {
            try
            {
                return await dbSet.Include(p => p.Client).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(FoundationRepository));
                return new List<Foundation>();
            }
        }

        public override async Task<bool> UpdateAsync(Foundation entity)
        {
            try
            {
                var existingentity = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

                if (existingentity == null)
                    return await AddAsync(entity);

                existingentity.NameFoundation = entity.NameFoundation;

                existingentity.CertificateNumber = entity.CertificateNumber;

                existingentity.BIC = entity.BIC;

                existingentity.ClientId = entity.ClientId;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(FoundationRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(FoundationRepository));
                return false;
            }
        }

    }
}
