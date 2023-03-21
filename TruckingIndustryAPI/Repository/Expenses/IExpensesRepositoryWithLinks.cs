namespace TruckingIndustryAPI.Repository.Expenses
{
    public interface IExpensesRepositoryWithLinks : IGenericRepository<Entities.Models.Expense>
    {
        Task<IEnumerable<Entities.Models.Expense>> GetByIdBidAsync(long IdBid);
    }
}
