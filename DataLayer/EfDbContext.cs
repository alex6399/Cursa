﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Entities.Bases;
using DataLayer.Seed;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class EfDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        private readonly HttpContext _httpContext;

        public EfDbContext(DbContextOptions<EfDbContext> options, IHttpContextAccessor contextAccessor)
            : base(options)
        {
            _httpContext = contextAccessor.HttpContext;
        }

        public override DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<SubProject> SubProjects { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductSubType> ProductSubTypes { get; set; }
        public DbSet<SystemUnit> SystemUnits { get; set; }
        public DbSet<SystemUnitType> SystemUnitTypes { get; set; }
        public DbSet<ModuleType> Modules { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }
        public DbSet<OrderCard> OrderCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
            
            //start many to many 
            modelBuilder.Entity<OrderEmployee>().HasKey(or
                => new {or.EmployeeId, or.OrderCardId, or.StatusParticipationId});
            modelBuilder.Entity<OrderEmployee>().ToTable("OrderEmployees");

            modelBuilder.Entity<OrderEmployee>()
                .HasOne(or => or.OrderCard)
                .WithMany(ea => ea.OrderEmployees)
                .HasForeignKey(or => or.OrderCardId);

            modelBuilder.Entity<OrderEmployee>()
                .HasOne(or => or.Employee)
                .WithMany(u => u.OrderEmployees)
                .HasForeignKey(or => or.EmployeeId);
            // end

            //Start: Configure One-to-Many User -> Project 
            modelBuilder.Entity<Project>()
                .HasOne<User>(p => p.CreatedUser)
                .WithMany(u => u.CreatedProjects)
                .HasForeignKey(p => p.CreatedUserId);
            modelBuilder.Entity<Project>()
                .HasOne<User>(p => p.ModifiedUser)
                .WithMany(u => u.ModifiedProjects)
                .HasForeignKey(p => p.ModifiedUserId);
            //End: Configure One-to-Many User -> Project

            //Start: Configure One-to-Many User -> SubProject 
            modelBuilder.Entity<SubProject>()
                .HasOne<User>(p => p.CreatedUser)
                .WithMany(u => u.CreatedSubProjects)
                .HasForeignKey(p => p.CreatedUserId);
            modelBuilder.Entity<SubProject>()
                .HasOne<User>(p => p.ModifiedUser)
                .WithMany(u => u.ModifiedSubProjects)
                .HasForeignKey(p => p.ModifiedUserId);
            //End: Configure One-to-Many User -> SubProject

            //Start: Configure One-to-Many User -> Contract 
            modelBuilder.Entity<Contract>()
                .HasOne<User>(p => p.CreatedUser)
                .WithMany(u => u.CreatedContracts)
                .HasForeignKey(p => p.CreatedUserId);
            modelBuilder.Entity<Contract>()
                .HasOne<User>(p => p.ModifiedUser)
                .WithMany(u => u.ModifiedContracts)
                .HasForeignKey(p => p.ModifiedUserId);
            //End: Configure One-to-Many User -> Contract

            //Start: Configure One-to-Many User -> Employee
            modelBuilder.Entity<Employee>()
                .HasOne<User>(p => p.CreatedUser)
                .WithMany(u => u.CreatedEmployees)
                .HasForeignKey(p => p.CreatedUserId);
            modelBuilder.Entity<Employee>()
                .HasOne<User>(p => p.ModifiedUser)
                .WithMany(u => u.ModifiedEmployees)
                .HasForeignKey(p => p.ModifiedUserId);
            //End: Configure One-to-Many User -> Employee

            //Start: Configure One-to-Many User -> Module
            modelBuilder.Entity<Module>()
                .HasOne<User>(p => p.CreatedUser)
                .WithMany(u => u.CreatedModules)
                .HasForeignKey(p => p.CreatedUserId);
            modelBuilder.Entity<Module>()
                .HasOne<User>(p => p.ModifiedUser)
                .WithMany(u => u.ModifiedModules)
                .HasForeignKey(p => p.ModifiedUserId);
            //End: Configure One-to-Many User -> Module

            //Start: Configure One-to-Many User -> Product
            modelBuilder.Entity<Product>()
                .HasOne<User>(p => p.CreatedUser)
                .WithMany(u => u.CreatedProducts)
                .HasForeignKey(p => p.CreatedUserId);
            modelBuilder.Entity<Product>()
                .HasOne<User>(p => p.ModifiedUser)
                .WithMany(u => u.ModifiedProducts)
                .HasForeignKey(p => p.ModifiedUserId);
            //End: Configure One-to-Many User -> Product

            //Start: Configure One-to-Many User -> User
            modelBuilder.Entity<User>()
                .HasOne<User>(p => p.CreatedUser)
                .WithMany(u => u.CreatedUsers)
                .HasForeignKey(p => p.CreatedUserId);
            modelBuilder.Entity<User>()
                .HasOne<User>(p => p.ModifiedUser)
                .WithMany(u => u.ModifiedUsers)
                .HasForeignKey(p => p.ModifiedUserId);
            //End: Configure One-to-Many User -> User


            //Start: Огрничение на каскадное удаление 
            modelBuilder.Entity<SubProject>()
                .HasOne(sp => sp.Project)
                .WithMany(p => p.SubProjects)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Entities.Product>()
                .HasOne(p => p.SubProject)
                .WithMany(o => o.Products)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Entities.Owner>()
                .HasMany(p => p.Projects)
                .WithOne(o => o.Owner)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Entities.Status>()
                .HasMany(p => p.SubProjects)
                .WithOne(o => o.Status)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Entities.StatusType>()
                .HasMany(p => p.Statuses)
                .WithOne(o => o.StatusType)
                .OnDelete(DeleteBehavior.Restrict);
            //End: Огрничение на каскадное удаление 

            // Start: unique constraint
            modelBuilder.Entity<Product>()
                .HasIndex(x => x.SerialNum)
                .IsUnique();
            modelBuilder.Entity<Product>()
                .HasIndex(x => x.CertifiedNum)
                .IsUnique();
            
            modelBuilder.Entity<Project>()
                .HasIndex(x => x.Name)
                .IsUnique();
            modelBuilder.Entity<Project>()
                .HasIndex(x => x.Code)
                .IsUnique();
            modelBuilder.Entity<SubProject>()
                .HasIndex(x => x.Name)
                .IsUnique();
            modelBuilder.Entity<SubProject>()
                .HasIndex(x => x.Code)
                .IsUnique();

            // End: unique constraint

            modelBuilder.Entity<Employee>().Property(p => p.CreatedDate).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<Department>().Property(p => p.CreatedDate).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<User>().Property(p => p.CreatedDate).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<Producer>().Property(p => p.CreatedDate).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<Contractor>().Property(p => p.CreatedDate).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<Owner>().Property(p => p.CreatedDate).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<Project>().Property(p => p.CreatedDate).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<SubProject>().Property(p => p.CreatedDate).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<OrderCard>().Property(p => p.CreatedDate).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<ProductType>().Property(p => p.CreatedDate).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<Status>().Property(p => p.CreatedDate).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<StatusType>().Property(p => p.CreatedDate).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<ModuleType>().Property(p => p.CreatedDate).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<Module>().Property(p => p.CreatedDate).HasDefaultValueSql("NOW()");
            modelBuilder.Entity<Product>().Property(p => p.CreatedDate).HasDefaultValueSql("NOW()");
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AddTrackingAndTimeInfo();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTrackingAndTimeInfo();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void AddTrackingAndTimeInfo()
        {
            var entities = ChangeTracker.Entries().Where(
                x => x.Entity is BaseEntityTracking
                     && (x.State == EntityState.Added || x.State == EntityState.Modified));
            var userId = Convert.ToInt32(_httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            foreach (var entity in entities)
            {
                if (entity.Entity is BaseEntityTracking baseEntityTracking)
                {
                    switch (entity.State)
                    {
                        case EntityState.Added:
                            baseEntityTracking.CreatedDate = DateTime.UtcNow;
                            baseEntityTracking.CreatedUserId = userId;
                            break;
                        case EntityState.Modified:
                            baseEntityTracking.ModifiedDate = DateTime.UtcNow;
                            baseEntityTracking.ModifiedUserId = userId;
                            break;
                    }
                }
            }
        }
    }
}