using Microsoft.EntityFrameworkCore;

using TruckingIndustryAPI.Data;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Repository.Employees;

namespace TruckingIndustryAPI.Repository.Cargos
{
    public class CargoRepositoryWithLinks : GenericRepository<Cargo>, ICargoRepositoryWithLinks
    {
        public CargoRepositoryWithLinks(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<bool> AddAsync(Cargo entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public async Task<double> GetTotalWeightByCarIdAsync(long carId)
        {
            // Получить все грузы для машины
            var cargoes = await dbSet.Include(i => i.Bids).Where(w => w.Bids.CarsId == carId).ToListAsync();

            // Суммировать веса грузов
            double sumWeight = cargoes.Sum(s => s.WeightCargo);

            return sumWeight;
        }

        public override async Task<IEnumerable<Cargo>> GetAllAsync()
        {
            try
            {
                return await dbSet.Include(i => i.TypeCargo).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(CargoRepositoryWithLinks));
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
                _logger.LogError(ex, "{Repo} Update function error", typeof(CargoRepositoryWithLinks));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(CargoRepositoryWithLinks));
                return false;
            }
        }

        public override async Task<Cargo> GetByIdAsync(long id)
        {
            try
            {
                return await dbSet.Include(e => e.TypeCargo).Where(n => n.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetById function error", typeof(EmployeeRepositoryWithLinks));
                return new Cargo();
            }
        }

        public async Task<IEnumerable<Cargo>> GetByIdBidAsync(long idBid)
        {
            try
            {
                var cargo = await dbSet.Include(i => i.TypeCargo).Where(x => x.BidsId == idBid).ToListAsync();
                return cargo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(CargoRepositoryWithLinks));
                return new List<Cargo>();
            }
        }
    }
}
