using Microsoft.EntityFrameworkCore;

using TruckingIndustryAPI.Data;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Repository.Cargos
{
    public class CargoRepository : GenericRepository<Cargo>, ICargoRepository
    {
        public CargoRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Cargo>> GetAllAsync()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(CargoRepository));
                return new List<Cargo>();
            }
        }

        public override async Task<bool> UpdateAsync(Cargo entity)
        {
            try
            {
                var existingentity = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

                if (existingentity == null)
                    return await AddAsync(entity);

                existingentity.NameCargo = entity.NameCargo;
                existingentity.WeightCargo = entity.WeightCargo;
                existingentity.TypeCargoId = entity.TypeCargoId;
                existingentity.BidsId = entity.BidsId;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(CargoRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(CargoRepository));
                return false;
            }
        }

        public async Task<IEnumerable<Cargo>> GetByIdBidAsync(long idBid)
        {
            try
            {
                var cargo = await dbSet.Where(x => x.BidsId == idBid).ToListAsync();
                return cargo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(CargoRepository));
                return new List<Cargo>();
            }
        }
    }
}
