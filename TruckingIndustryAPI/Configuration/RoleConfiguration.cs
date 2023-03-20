using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TruckingIndustryAPI.Entities.Models.Identity;

namespace TruckingIndustryAPI.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(
                new ApplicationRole
                {
                    Name = "Viewer",
                    NormalizedName = "VIEWER",
                    RoleInRussian = "Просмотр"
                },
                new ApplicationRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    RoleInRussian = "Администратор"
                },
                new ApplicationRole
                {
                    Name = "Moderator",
                    NormalizedName = "MODERATOR",
                    RoleInRussian = "Модератор"
                }
            );
        }
    }
}
