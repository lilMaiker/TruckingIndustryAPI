using Microsoft.AspNetCore.Identity;

using TruckingIndustryAPI.Entities.Models.Identity;
using TruckingIndustryAPI.Repository.ApplicationRole;
using TruckingIndustryAPI.Repository.ApplicationUserRoles;
using TruckingIndustryAPI.Repository.ApplicationUsers;
using TruckingIndustryAPI.Repository.Bids;
using TruckingIndustryAPI.Repository.Cargos;
using TruckingIndustryAPI.Repository.Cars;
using TruckingIndustryAPI.Repository.Clients;
using TruckingIndustryAPI.Repository.Currency;
using TruckingIndustryAPI.Repository.Employees;
using TruckingIndustryAPI.Repository.Expenses;
using TruckingIndustryAPI.Repository.Foundations;
using TruckingIndustryAPI.Repository.Positions;
using TruckingIndustryAPI.Repository.Routes;
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
        IClientRepository Client { get; }
        IFoundationRepository Foundation { get; }
        IBidsRepository Bids { get; }
        ICargoRepository Cargo { get; }
        IExpensesRepository Expenses { get; }
        IRouteRepository Route { get; }
        IApplicationUserRepository User { get; }
        IApplicationRoleRepository Role { get; }
        IApplicationUserRolesRepository UserRoles { get; }
        Task CompleteAsync();
    }
}
