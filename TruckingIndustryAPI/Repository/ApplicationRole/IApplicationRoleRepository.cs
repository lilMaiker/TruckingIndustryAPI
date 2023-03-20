namespace TruckingIndustryAPI.Repository.ApplicationRole
{
    public interface IApplicationRoleRepository : IGenericRepository<Entities.Models.Identity.ApplicationRole>
    {
        Task<Entities.Models.Identity.ApplicationRole> GetByIdAsync(string id);
    }
}
