using Microsoft.AspNetCore.Identity;

namespace TruckingIndustryAPI.Entities.Models.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public string? RoleInRussian { get; set; }
    }
}
