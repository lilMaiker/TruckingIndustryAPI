using Microsoft.EntityFrameworkCore;

using TruckingIndustryAPI.Data;
using TruckingIndustryAPI.Repository.Employees;

namespace TruckingIndustryAPI.Repository.ApplicationRole
{
    public class ApplicationRoleRepository : GenericRepository<Entities.Models.Identity.ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public async Task<Entities.Models.Identity.ApplicationRole> GetByIdAsync(string id)
        {
            try
            {
                return await dbSet.Where(n => n.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetById function error", typeof(EmployeeRepositoryWithLinks));
                return new Entities.Models.Identity.ApplicationRole();
            }
        }

        public override async Task<IEnumerable<Entities.Models.Identity.ApplicationRole>> GetAllAsync()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(ApplicationRoleRepository));
                return new List<Entities.Models.Identity.ApplicationRole>();
            }
        }
    }
}
