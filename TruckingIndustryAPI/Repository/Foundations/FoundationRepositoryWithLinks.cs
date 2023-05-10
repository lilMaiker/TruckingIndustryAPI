using Microsoft.EntityFrameworkCore;

using TruckingIndustryAPI.Data;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Repository.Employees;

namespace TruckingIndustryAPI.Repository.Foundations
{
    public class FoundationRepositoryWithLinks : GenericRepository<Foundation>, IFoundationRepositoryWithLinks
    {
        public FoundationRepositoryWithLinks(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<Foundation> GetByIdAsync(long id)
        {
            try
            {
                return await dbSet.Include(e => e.Client).Include(e => e.Sector).Where(n => n.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetById function error", typeof(EmployeeRepositoryWithLinks));
                return new Foundation();
            }
        }

        public override async Task<IEnumerable<Foundation>> GetAllAsync()
        {
            try
            {
                return await dbSet.Include(p => p.Client).Include(e => e.Sector).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(FoundationRepositoryWithLinks));
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
                existingentity.SectorId = entity.SectorId;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(FoundationRepositoryWithLinks));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(FoundationRepositoryWithLinks));
                return false;
            }
        }

    }
}
