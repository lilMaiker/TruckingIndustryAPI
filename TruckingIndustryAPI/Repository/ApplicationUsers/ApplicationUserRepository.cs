using Microsoft.EntityFrameworkCore;

using TruckingIndustryAPI.Data;
using TruckingIndustryAPI.Repository.Bids;

namespace TruckingIndustryAPI.Repository.ApplicationUsers
{
    public class ApplicationUserRepository : GenericRepository<Entities.Models.Identity.ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Entities.Models.Identity.ApplicationUser>> GetAllAsync()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(ApplicationUserRepository));
                return new List<Entities.Models.Identity.ApplicationUser>();
            }
        }

      
    }
}
