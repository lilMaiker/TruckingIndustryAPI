namespace TruckingIndustryAPI.Repository.Expenses
{
    public interface IExpensesRepository : IGenericRepository<Entities.Models.Expenses>
    {
        Task<IEnumerable<Entities.Models.Expenses>> GetByIdBidAsync(long IdBid);
    }
}
