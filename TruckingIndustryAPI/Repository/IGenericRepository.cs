using System.Linq.Expressions;

namespace TruckingIndustryAPI.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        Task<bool> AddAsync(T entity);
        Task<bool> DeleteAsync(long id);
        Task<bool> UpdateAsync(T entity);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
