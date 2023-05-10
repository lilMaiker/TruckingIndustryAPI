using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TruckingIndustryAPI.Entities.Models;

namespace TruckingIndustryAPI.Configuration
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasData(
                new Status
                {
                    Id = 1L,
                    NameStatus = "В обработке"
                },
                 new Status
                 {
                     Id = 2L,
                     NameStatus = "В пути"
                 },
                  new Status
                  {
                      Id = 3L,
                      NameStatus = "Отгружен"
                  },
                   new Status
                   {
                       Id = 4L,
                       NameStatus = "Подготовка к отгрузке"
                   },
                    new Status
                    {
                        Id = 5L,
                        NameStatus = "Подготовка к загрузке"
                    },
                     new Status
                     {
                         Id = 6L,
                         NameStatus = "Загружен"
                     },
                      new Status
                      {
                          Id = 7L,
                          NameStatus = "Подготовка документов"
                      },
                      new Status
                      {
                          Id = 8L,
                          NameStatus = "Не выбран"
                      }
            );
        }
    }
}