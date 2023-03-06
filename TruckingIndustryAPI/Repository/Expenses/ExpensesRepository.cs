using Microsoft.EntityFrameworkCore;

using TruckingIndustryAPI.Data;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Repository.Cargos;

namespace TruckingIndustryAPI.Repository.Expenses
{
    public class ExpensesRepository : GenericRepository<Entities.Models.Expenses>, IExpensesRepository
    {
        public ExpensesRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Entities.Models.Expenses>> GetAllAsync()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(ExpensesRepository));
                return new List<Entities.Models.Expenses>();
            }
        }

        public override async Task<bool> UpdateAsync(Entities.Models.Expenses entity)
        {
            try
            {
                var existingentity = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

                if (existingentity == null)
                    return await AddAsync(entity);

                existingentity.NameExpense = entity.NameExpense;

                existingentity.Amount = entity.Amount;

                existingentity.CurrencyId = entity.CurrencyId;

                existingentity.DateTransfer = entity.DateTransfer;

                existingentity.Commnet = entity.Commnet;

                existingentity.BidsId = entity.BidsId;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(ExpensesRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(ExpensesRepository));
                return false;
            }
        }

        public async Task<IEnumerable<Entities.Models.Expenses>> GetByIdBidAsync(long idBid)
        {
            try
            {
                var expenses = await dbSet.Where(x => x.BidsId == idBid).ToListAsync();
                return expenses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(ExpensesRepository));
                return new List<Entities.Models.Expenses>();
            }
        }

    }
}
