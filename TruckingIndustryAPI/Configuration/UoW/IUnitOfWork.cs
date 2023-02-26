using Microsoft.AspNetCore.Identity;

using TruckingIndustryAPI.Entities.Models.Identity;
using TruckingIndustryAPI.Repository.Positions;

namespace TruckingIndustryAPI.Configuration.UoW
{
    public interface IUnitOfWork
    {
        UserManager<ApplicationUser> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        IPositionRepository Position { get; }
        Task CompleteAsync();
    }
}
