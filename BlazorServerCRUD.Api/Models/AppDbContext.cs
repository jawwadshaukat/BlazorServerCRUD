using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorServerCRUD.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BlazorServerCRUD.Models;
using BlazorServerCRUD.Api;
using System.ComponentModel.DataAnnotations; ///Missing

namespace BlazorServerCRUD.Api.Models
{
   public class AppDbContext : DbContext 
   {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options: options)
        { 
        }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        //specify database provider by overriding OnConfiguring method

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Department
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentID = 1, DepartmentName = "Admin" });
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentID = 2, DepartmentName = "HR" });
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentID = 3, DepartmentName = "Payroll" });

            //Employee
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeID = 1,
                EmployeeName = "John",
                DateOfBirth = new DateTime(1990, 08, 05),
                Gender = Gender.Male,
                DepartmentID = 1
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeID = 2,
                EmployeeName = "Sally",
                DateOfBirth = new DateTime(1980, 03, 02),
                Gender = Gender.Female,
                DepartmentID = 3
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeID = 3,
                EmployeeName = "Ali",
                DateOfBirth = new DateTime(1995, 05, 05),
                Gender = Gender.Male,
                DepartmentID = 2
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeID = 4,
                EmployeeName = "Tony",
                DateOfBirth = new DateTime(1989, 01, 01),
                Gender = Gender.Male,
                DepartmentID = 3

            });

        }
        
    }

   
}
