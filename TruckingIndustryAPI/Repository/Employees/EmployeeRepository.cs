using Microsoft.EntityFrameworkCore;

using TruckingIndustryAPI.Data;
using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Repository.Employees
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<Employee> GetByIdAsync(long id)
        {
            try
            {
                return await dbSet.Include(e => e.Position).Include(e => e.ApplicationUser).Where(n => n.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetById function error", typeof(EmployeeRepository));
                return new Employee();
            }
        }

        public override async Task<IEnumerable<Employee>> GetAllAsync()
        {
            try
            {
                return await dbSet.Include(e => e.Position).Include(e => e.ApplicationUser).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(EmployeeRepository));
                return new List<Employee>();
            }
        }

        public override async Task<bool> UpdateAsync(Employee entity)
        {
            try
            {
                var existingentity = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

                if (existingentity == null)
                    return await AddAsync(entity);

                existingentity.Surname = entity.Surname;
                existingentity.Name = entity.Name;
                existingentity.Patronymic = entity.Patronymic;
                existingentity.Email = entity.Email;
                existingentity.PositionId = entity.PositionId;
                existingentity.ApplicationUserId = entity.ApplicationUserId;
                existingentity.PassportNumber = entity.PassportNumber;
                existingentity.PhoneNumber = entity.PhoneNumber;
                existingentity.SerialNumber = entity.SerialNumber;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(EmployeeRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(EmployeeRepository));
                return false;
            }
        }
    }
}
