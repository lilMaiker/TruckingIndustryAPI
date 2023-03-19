namespace TruckingIndustryAPI.Repository.Expenses
{
    public interface IExpensesRepository : IGenericRepository<Entities.Models.Expense>
    {
        Task<IEnumerable<Entities.Models.Expense>> GetByIdBidAsync(long IdBid);
    }
}
