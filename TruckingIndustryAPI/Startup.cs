using Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

using TruckingIndustryAPI.Data;
using TruckingIndustryAPI.Entities.Models.Identity;
using TruckingIndustryAPI.Extensions;
using TruckingIndustryAPI.Helpers;
using TruckingIndustryAPI.Services.Email;

namespace TruckingIndustryAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Интеграция с IIS для прокси-сервера или InProcess-хостинга.
            services.ConfigureIISIntegration();

            // Конфигурирование контекста базы данных.
            services.ConfigureSqlContext(Configuration);

            // Конфигурирование репозитория и всех доступных ему репозиториев.
            services.ConfigureRepositoryManager();


            services.ConfigureCors();

            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

            // Добавление AutoMapper в контейнер DI.
            services.AddAutoMapper(typeof(Startup));

            // Регистрация контроллеров как служб в контейнере DI.
            services.AddControllers();

            // Конфигурирование логирования.
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole()
                             .AddDebug()
                             .AddEventSourceLogger();
            });

            // Конфигурирование авторизации и аутентификации.
            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 7;
                opt.Password.RequireDigit = false;

                opt.User.RequireUniqueEmail = true;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                opt.Lockout.MaxFailedAccessAttempts = 3;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));

            var jwtSettings = Configuration.GetSection("JwtSettings");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(jwtSettings.GetSection("securityKey").Value))
                };
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSwaggerGen(c =>
            {
                /* c.SwaggerDoc("v1", new OpenApiInfo { Title = "SupportService API v1", Version = "v1" });*/
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Регистрация службы JwtHandler с ограниченным временем жизни (scoped lifetime).
            services.AddScoped<JwtHandler>();

            // Получение настроек электронной почты из конфигурационного файла и регистрация их как singleton-службы в контейнере DI.
            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            // Регистрация службы EmailSender с ограниченным временем жизни (scoped lifetime) и интерфейсом IEmailSender.
            services.AddScoped<IEmailSender, EmailSender>();

            // Добавление службы, которая генерирует OpenAPI-спецификацию для конечных точек приложения, зарегистрированных в контейнере DI.
            services.AddEndpointsApiExplorer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TruckingIndustry API v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
