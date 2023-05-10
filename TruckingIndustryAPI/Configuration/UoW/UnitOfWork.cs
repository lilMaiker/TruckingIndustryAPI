using Microsoft.AspNetCore.Identity;

using TruckingIndustryAPI.Data;
using TruckingIndustryAPI.Entities.Models.Identity;
using TruckingIndustryAPI.Repository.ApplicationRole;
using TruckingIndustryAPI.Repository.ApplicationUserRoles;
using TruckingIndustryAPI.Repository.ApplicationUsers;
using TruckingIndustryAPI.Repository.Bids;
using TruckingIndustryAPI.Repository.Cargos;
using TruckingIndustryAPI.Repository.Cars;
using TruckingIndustryAPI.Repository.Clients;
using TruckingIndustryAPI.Repository.Currencies;
using TruckingIndustryAPI.Repository.Currency;
using TruckingIndustryAPI.Repository.Employees;
using TruckingIndustryAPI.Repository.Expenses;
using TruckingIndustryAPI.Repository.Foundations;
using TruckingIndustryAPI.Repository.Positions;
using TruckingIndustryAPI.Repository.Routes;
using TruckingIndustryAPI.Repository.SectorsRepository;
using TruckingIndustryAPI.Repository.Status;
using TruckingIndustryAPI.Repository.TypeCargos;

namespace TruckingIndustryAPI.Configuration.UoW
{

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IPositionRepository Positions { get; private set; }
        public IEmployeeRepositoryWithLinks Employees { get; private set; }
        public IStatusRepository Status { get; private set; }
        public ICurrencyRepository Currency { get; private set; }
        public ITypeCargoRepository TypeCargo { get; private set; }
        public ICarsRepository Cars { get; private set; }
        public IClientRepository Client { get; private set; }
        public IFoundationRepositoryWithLinks Foundation { get; private set; }
        public IBidsRepositoryWithLinks Bids { get; private set; }
        public ICargoRepositoryWithLinks Cargo { get; private set; }
        public IExpensesRepositoryWithLinks Expenses { get; private set; }
        public IRouteRepository Route { get; private set; }
        public IApplicationUserRepository User { get; private set; }
        public IApplicationRoleRepository Role { get; private set; }
        public IApplicationUserRolesRepository UserRoles { get; private set; }
        public ISectorRepository Sector { get; private set; }

        public UnitOfWork(ApplicationDbContext context,
            ILoggerFactory loggerFactory, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            _userManager = userManager;
            _roleManager = roleManager;

            Positions = new PositionRepository(context, _logger);
            Currency = new CurrencyRepository(context, _logger);
            Employees = new EmployeeRepositoryWithLinks(context, _logger);
            Status = new StatusRepository(context, _logger);
            TypeCargo = new TypeCargoRepository(context, _logger);
            Cars = new CarsRepository(context, _logger);
            Client = new ClientRepository(context, _logger);
            Foundation = new FoundationRepositoryWithLinks(context, _logger);
            Bids = new BidsRepositoryWithLinks(context, _logger);
            Cargo = new CargoRepositoryWithLinks(context, _logger);
            Expenses = new ExpensesRepositoryWithLinks(context, _logger);
            Route = new RouteRepository(context, _logger);
            User = new ApplicationUserRepository(context, _logger);
            Role = new ApplicationRoleRepository(context, _logger);
            UserRoles = new ApplicationUserRolesRepository(context, _logger);
            Sector = new SectorRepository(context, _logger);
        }

        public UserManager<ApplicationUser> UserManager
        {
            get { return _userManager; }
        }

        public RoleManager<IdentityRole> RoleManager
        {
            get { return _roleManager; }
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
