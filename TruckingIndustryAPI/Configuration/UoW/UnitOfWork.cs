using Microsoft.AspNetCore.Identity;
using TruckingIndustryAPI.Data;
using TruckingIndustryAPI.Entities.Models.Identity;

namespace TruckingIndustryAPI.Configuration.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
            _userManager = userManager;

            _roleManager = roleManager;
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
