using Microsoft.AspNetCore.Identity;
using TruckingIndustryAPI.Data;
using TruckingIndustryAPI.Entities.Models.Identity;
using TruckingIndustryAPI.Extensions.Attributes;
using TruckingIndustryAPI.Repository.Cars;
using TruckingIndustryAPI.Repository.Currencies;
using TruckingIndustryAPI.Repository.Currency;
using TruckingIndustryAPI.Repository.Employees;
using TruckingIndustryAPI.Repository.Positions;
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
        public IEmployeeRepository Employees { get; private set; }
        public IStatusRepository Status { get; private set; }
        public ICurrencyRepository Currency { get; private set; }
        public ITypeCargoRepository TypeCargo { get; private set; }
        public ICarsRepository Cars { get; private set; }

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
            Employees = new EmployeeRepository(context, _logger);
            Status = new StatusRepository(context, _logger);
            TypeCargo = new TypeCargoRepository(context, _logger);
            Cars = new CarsRepository(context, _logger);
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
