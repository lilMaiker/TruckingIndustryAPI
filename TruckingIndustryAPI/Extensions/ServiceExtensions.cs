using Google;
using Microsoft.EntityFrameworkCore;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Data;

namespace TruckingIndustryAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
           services.AddCors(options =>
           {
               options.AddPolicy("CorsPolicy", builder =>
                   builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());
           });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {

            });

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(
                     configuration.GetConnectionString("DefaultConnection")));

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
           services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
