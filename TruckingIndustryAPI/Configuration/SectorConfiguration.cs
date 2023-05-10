using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Configuration
{
    public class SectorConfiguration : IEntityTypeConfiguration<Sector>
    {
        public void Configure(EntityTypeBuilder<Sector> builder)
        {
            builder.HasData(
                new Sector
                {
                    Id = 1L,
                    NameSector = "Нефтегазовый"
                },
                new Sector
                {
                    Id = 2L,
                    NameSector = "Нефтехимический"
                },
                new Sector
                {
                    Id = 3L,
                    NameSector = "Телекоммуникационный"
                },
                new Sector
                {
                    Id = 4L,
                    NameSector = "IT"
                },
                new Sector
                {
                    Id = 5L,
                    NameSector = "Добывающий"
                },
                new Sector
                {
                    Id = 6L,
                    NameSector = "Финансовый"
                }
            );
        }
    }
}