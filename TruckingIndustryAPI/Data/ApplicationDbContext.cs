﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;

using TruckingIndustryAPI.Configuration;
using TruckingIndustryAPI.Entities.Models;
using TruckingIndustryAPI.Entities.Models.Identity;

namespace TruckingIndustryAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // The DbSet property will tell EF Core tha we have a table that needs to be created
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Cars> Cars { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // On model creating function will provide us with the ability to manage the tables properties
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            //Статусы
            //Валюты
            //Тип груза
        }
    }
}
