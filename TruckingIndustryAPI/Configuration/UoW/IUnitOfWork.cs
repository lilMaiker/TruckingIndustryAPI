using Microsoft.AspNetCore.Identity;

using TruckingIndustryAPI.Entities.Models.Identity;
using TruckingIndustryAPI.Repository.Cars;
using TruckingIndustryAPI.Repository.Currency;
using TruckingIndustryAPI.Repository.Employees;
using TruckingIndustryAPI.Repository.Positions;
using TruckingIndustryAPI.Repository.Status;
using TruckingIndustryAPI.Repository.TypeCargos;

namespace TruckingIndustryAPI.Configuration.UoW
{
    public interface IUnitOfWork
    {
        UserManager<ApplicationUser> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        IPositionRepository Positions { get; }
        IStatusRepository Status { get; }
        ICarsRepository Cars { get; }
        ICurrencyRepository Currency { get; }
        ITypeCargoRepository TypeCargo { get; }
        IEmployeeRepository Employees { get; }
        Task CompleteAsync();
    }
}
