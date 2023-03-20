using Microsoft.EntityFrameworkCore;

using TruckingIndustryAPI.Data;

namespace TruckingIndustryAPI.Repository.ApplicationUserRoles
{
    public class ApplicationUserRolesRepository : GenericRepository<Entities.Models.Identity.ApplicationUserRoles>, IApplicationUserRolesRepository
    {
        public ApplicationUserRolesRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Entities.Models.Identity.ApplicationUserRoles>> GetAllAsync()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(ApplicationUserRolesRepository));
                return new List<Entities.Models.Identity.ApplicationUserRoles>();
            }
        }

    }
}
