using Microsoft.AspNetCore.Identity;

using TruckingIndustryAPI.Entities.Models.Identity;

namespace TruckingIndustryAPI.Configuration.UoW
{
    public interface IUnitOfWork
    {
        UserManager<ApplicationUser> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }

        Task CompleteAsync();
    }
}
