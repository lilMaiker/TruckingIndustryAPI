using Microsoft.EntityFrameworkCore;
using TruckingIndustryAPI.Data;
using TruckingIndustryAPI.Repository.Currency;

namespace TruckingIndustryAPI.Repository.Currencies
{
    public class CurrencyRepository : GenericRepository<Entities.Models.Currency>, ICurrencyRepository
    {
        public CurrencyRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Entities.Models.Currency>> GetAllAsync()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(CurrencyRepository));
                return new List<Entities.Models.Currency>();
            }
        }

        public override async Task<bool> UpdateAsync(Entities.Models.Currency entity)
        {
            try
            {
                var existingentity = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

                if (existingentity == null)
                    return await AddAsync(entity);

                existingentity.NameCurrency = entity.NameCurrency;
                existingentity.CurrencyCode = entity.CurrencyCode;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(CurrencyRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(CurrencyRepository));
                return false;
            }
        }

    }
}
