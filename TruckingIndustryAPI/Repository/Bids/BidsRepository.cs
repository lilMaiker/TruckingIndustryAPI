using Microsoft.EntityFrameworkCore;

using TruckingIndustryAPI.Data;
using TruckingIndustryAPI.Repository.Employees;

namespace TruckingIndustryAPI.Repository.Bids
{
    public class BidsRepository : GenericRepository<Entities.Models.Bid>, IBidsRepository
    {
        public BidsRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<Entities.Models.Bid> GetByIdAsync(long id)
        {
            try
            {
                return await dbSet.Include(e => e.Cars).Include(e => e.Currency).Include(e => e.Employee).Include(e => e.Foundation).Include(e => e.Status).Where(n => n.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetById function error", typeof(EmployeeRepository));
                return new Entities.Models.Bid();
            }
        }

        public override async Task<IEnumerable<Entities.Models.Bid>> GetAllAsync()
        {
            try
            {
                return await dbSet.Include(e => e.Cars).Include(e => e.Currency).Include(e => e.Employee).Include(e => e.Foundation).Include(e => e.Status).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(BidsRepository));
                return new List<Entities.Models.Bid>();
            }
        }

        public override async Task<bool> UpdateAsync(Entities.Models.Bid entity)
        {
            try
            {
                var existingentity = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

                if (existingentity == null)
                    return await AddAsync(entity);

                existingentity.CarsId = entity.CarsId;

                existingentity.FoundationId = entity.FoundationId;

                existingentity.FreightAMount = entity.FreightAMount;

                existingentity.CurrencyId = entity.CurrencyId;

                existingentity.DateToLoad = entity.DateToLoad;

                existingentity.DateToUnload = entity.DateToUnload;

                existingentity.ActAccNumber = entity.ActAccNumber;

                existingentity.StatusId = entity.StatusId;

                existingentity.PayDate = entity.PayDate;

                existingentity.EmployeeId = entity.EmployeeId;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(BidsRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(BidsRepository));
                return false;
            }
        }

    }
}
