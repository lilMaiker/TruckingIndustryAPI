using Microsoft.AspNetCore.Identity;
using TruckingIndustryAPI.Data;
using TruckingIndustryAPI.Entities.Models.Identity;
using TruckingIndustryAPI.Repository.Positions;

namespace TruckingIndustryAPI.Configuration.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IPositionRepository Position { get; private set; }

        public UnitOfWork(ApplicationDbContext context, 
            LoggerFactory loggerFactory, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            _userManager = userManager;
            _roleManager = roleManager;

            Position = new PositionRepository(context, _logger);
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
