#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GCourrier.Shared;

namespace GCourrier.Server.Data
{
    public class GCourrierDbContext : DbContext
    {
        public GCourrierDbContext (DbContextOptions<GCourrierDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<GCourrier.Shared.Department>()
              .Property(Department => Department.Id).ValueGeneratedOnAdd();
            //Seed Departments Table
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "IT" });
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 2, Name = "HR" });
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 3, Name = "Marketing" });
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 4, Name = "Admin" });





            modelBuilder.Entity<GCourrier.Shared.Agent>()
               .Property(Agent => Agent.Id).ValueGeneratedOnAdd();
            // Seed Agent Table
            modelBuilder.Entity<Agent>().HasData(new Agent
            {

                Id=1,
                FirstName = "Abd",
                LastName = "ElMonem",
                Email = "abd@samobay.com",
                
                Power = Power.SuperUser,
                DepartmentId = 1,
                PhotoPath = "images/abd.png"
            });

            modelBuilder.Entity<Agent>().HasData(new Agent
            {
                Id = 2,
                FirstName = "Said",
                LastName = "Sassi",
                Email = "said@amobay.com",
                
                Power = Power.SuperUser,
                DepartmentId = 2,
                PhotoPath = "images/said.jpg"
            });

            modelBuilder.Entity<Agent>().HasData(new Agent
            {
                Id = 3,
                FirstName = "Aymen",
                LastName = "Aymen",
                Email = "aymen@samobay.com",
                
                Power = Power.SuperUser,
                DepartmentId = 3,
                PhotoPath = "images/aymen.png"
            });

            modelBuilder.Entity<Agent>().HasData(new Agent
            {
                Id = 4,
                FirstName = "Idriss",
                LastName = "Laabidi",
                Email = "idriss@samobay.com",
                
                Power = Power.Admin,
                DepartmentId = 4,
                PhotoPath = "images/idriss.png"
            });







        }

        public DbSet<GCourrier.Shared.Agent> Agent { get; set; }

        public DbSet<GCourrier.Shared.Department> Department { get; set; }
    }
}
