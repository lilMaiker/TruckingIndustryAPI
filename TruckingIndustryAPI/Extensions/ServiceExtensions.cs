using Microsoft.EntityFrameworkCore;

using System.Reflection;

using TruckingIndustryAPI.Configuration.UoW;
using TruckingIndustryAPI.Data;
using TruckingIndustryAPI.Extensions.Attributes;

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

        public static void RegisterServices(this IServiceCollection services, Assembly assembly)
        {
            var serviceTypes = assembly.GetTypes().Where(t => t.Name.EndsWith("Service"));

            foreach (var type in serviceTypes)
            {
                var interfaces = type.GetInterfaces();
                var serviceType = interfaces.FirstOrDefault(i => i.Name.EndsWith("Service"));
                var implementationType = type;

                if (serviceType == null)
                {
                    serviceType = interfaces.FirstOrDefault();
                }

                if (serviceType == null)
                {
                    continue;
                }

                var attribute = implementationType.GetCustomAttribute<ServiceLifetimeAttribute>();

                if (attribute == null)
                {
                    services.AddTransient(serviceType, implementationType);
                }
                else
                {
                    switch (attribute.Lifetime)
                    {
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(serviceType, implementationType);
                            break;
                        case ServiceLifetime.Scoped:
                            services.AddScoped(serviceType, implementationType);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(serviceType, implementationType);
                            break;
                    }
                }
            }
        }
    }
}
