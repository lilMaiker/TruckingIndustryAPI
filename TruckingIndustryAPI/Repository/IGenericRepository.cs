using System.Linq.Expressions;

namespace TruckingIndustryAPI.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> All();
        Task<T> GetById(long id);
        Task<bool> Add(T entity);
        Task<bool> Delete(long id);
        Task<bool> Upsert(T entity);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    }
}
