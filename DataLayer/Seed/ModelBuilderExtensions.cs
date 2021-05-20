using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Seed
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityRole<int>>().HasData(
            //    new IdentityRole<int>
            //    {
            //        Id = 1,
            //        Name = "Администратор",
            //        NormalizedName = "АДМИНИСТРАТОР"

            //    },
            //    new IdentityRole<int>
            //    {
            //        Id = 2,
            //        Name = "Менеджер",
            //        NormalizedName = "МЕНЕДЖЕР"
            //    }
            //);
            //var hasher = new PasswordHasher<User>();
            //modelBuilder.Entity<User>().HasData(
            //    new User
            //    {
            //        Id = 1,
            //        UserName = "admin@admin.com",
            //        NormalizedUserName = "admin@admin.com",
            //        Email = "admin@admin.com",
            //        FirstName = "Админ",
            //        MiddleName = "Админ",
            //        LastName = "Админ",
            //        IsLockout = true,
            //        SecurityStamp = "",
            //        PasswordHash = hasher.HashPassword(null, "Password0-")
            //    }
            //);
            //modelBuilder.Entity<IdentityUserRole<int>>().HasData(
            //    new IdentityUserRole<int>
            //    {
            //        RoleId = 1,
            //        UserId = 1
            //    }
            //);

            modelBuilder.Entity<ModuleSubTypes>().HasData(
                new ModuleSubTypes
                {
                    Id = 1,
                    Name = "УСО аналоговый"
                    
                },
                new ModuleSubTypes
                {
                    Id = 2,
                    Name = "УСО дискретный"
                    
                },
                new ModuleSubTypes
                {
                    Id = 3,
                    Name = "Блок системный"
                }
            );
            modelBuilder.Entity<ModuleType>().HasData(
                new ModuleType
                {
                    Id = 1,
                    Name = "IA - 4k42 - M ",
                    ModuleSubTypesId = 1,
                    Code="ЛДАР.468155.047",
                    CountChanel = 4,
                    IsActiv=true,
                    IsSystem = true
                },
                new ModuleType
                {
                    Id = 2,
                    Name = "IA-8k42",
                    ModuleSubTypesId = 1,
                    Code="ЛДАР.468155.049",
                    CountChanel = 8,
                    IsActiv=true,
                    IsSystem = true
                },
                new ModuleType
                {
                    Id = 3,
                    Name = "OA - 4k42 - M",
                    ModuleSubTypesId = 1,
                    Code="ЛДАР.468155.046",
                    CountChanel = 4,
                    IsActiv=true,
                    IsSystem = true
                },
                new ModuleType
                {
                    Id = 4,
                    Name = "ID - 8k24 - M ",
                    ModuleSubTypesId = 2,
                    Code="ЛДАР.469219.043",
                    CountChanel = 8,
                    IsActiv=true,
                    IsSystem = true
                },
                new ModuleType
                {
                    Id = 5,
                    Name = "ID - 16k24 ",
                    ModuleSubTypesId = 2,
                    Code="ЛДАР.469219.060",
                    CountChanel = 16,
                    IsActiv=true,
                    IsSystem = true
                },
                new ModuleType
                {
                    Id = 6,
                    Name = "OD - 5k - M ",
                    ModuleSubTypesId = 2,
                    Code="ЛДАР.468154.050",
                    CountChanel = 5,
                    IsActiv=true,
                    IsSystem = true
                },
                new ModuleType
                {
                    Id = 7,
                    Name = "ОD - 16k24 ",
                    ModuleSubTypesId = 1,
                    Code="ЛДАР.468154.055",
                    CountChanel = 16,
                    IsActiv=true,
                    IsSystem = true
                },
                new ModuleType
                {
                    Id = 8,
                    Name = "IF - 3k ",
                    ModuleSubTypesId = 1,
                    Code="ЛДАР.468155.048",
                    CountChanel = 3,
                    IsActiv=true,
                    IsSystem = true
                },
                new ModuleType
                {
                    Id = 9,
                    Name = "USB/RS-485-4k",
                    ModuleSubTypesId = 1,
                    Code="ЛДАР.469239.104",
                    CountChanel = 0,
                    IsActiv=true,
                    IsSystem = true
                },
                new ModuleType
                {
                    Id = 10,
                    Name = "Com/RS-485",
                    ModuleSubTypesId = 1,
                    Code="ЛДАР.469239.299",
                    CountChanel = 0,
                    IsActiv=true,
                    IsSystem = true
                },
                new ModuleType
                {
                    Id = 11,
                    Name = "LPBS-15-М",
                    ModuleSubTypesId = 3,
                    Code="ЛДАР.469239.235",
                    CountChanel = 0,
                    IsActiv=true,
                    IsSystem = true
                }
            );

            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    Name = "Конструкторский",
                    IsSystem = true
                    
                },
                new Department
                {
                    Id = 2,
                    Name = "Управление",
                    IsSystem = true
                }
            );


            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id=1,
                    FirstName = "Андрей",
                    MiddleName = "Александрович",
                    LastName = "Вашин",
                    Phone = "+79173155974",
                    DepartmentId = 1
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Никита",
                    MiddleName = "Петрович",
                    LastName = "Кузебердин",
                    Phone = "+79273180075",
                    DepartmentId = 1
                },
                new Employee
                {
                    Id = 3,
                    FirstName = "Олег",
                    MiddleName = "Сергеевич",
                    LastName = "Хватов",
                    Phone = "+79193955666",
                    DepartmentId = 2
                }
            );



            modelBuilder.Entity<Owner>().HasData(
                new Owner
                {
                    Id = 1,
                    Name = "Комита"
                },
                new Owner
                {
                    Id = 2,
                    Name = "Элна"
                },
                new Owner
                {
                    Id = 3,
                    Name = "ООО АО"
                },
                new Owner
                {
                    Id = 4,
                    Name = "ОАО"
                },
                new Owner
                {
                    Id = 5,
                    Name = "СГС"
                }
            );

           


            modelBuilder.Entity<Contractor>().HasData(
                new Contractor
                {
                    Id = 1,
                    Name = "Онимет, АО"
                },
                new Contractor
                {
                    Id = 2,
                    Name = "Газпром автоматизация, ООО"
                },
                new Contractor
                {
                    Id = 3,
                    Name = "ООО \"Лидер\""
                });
            
            modelBuilder.Entity<ProductSubType>().HasData(
                new ProductSubType
                {
                    Id = 1,
                    Name = "АСУ"
                },
                new ProductSubType
                {
                    Id = 2,
                    Name = "САУ"
                },
                new ProductSubType
                {
                    Id = 3,
                    Name = "Система пожарной автоматики"
                },
                new ProductSubType
                {
                    Id = 4,
                    Name = "Система учета энергоресурсов"
                },
                new ProductSubType
                {
                    Id = 5,
                    Name = "Системы локальной автоматики"
                },
                new ProductSubType
                {
                    Id = 6,
                    Name = "Телемеханика"
                });

            modelBuilder.Entity<ProductType>().HasData(
                new ProductType
                {
                    Id = 1,
                    Name = "УСО РУ",
                    ProductSubTypeId = 1
                },
                new ProductType
                {
                    Id = 2,
                    Name = "САУ К",
                    ProductSubTypeId = 1
                },
                new ProductType
                {
                    Id = 3,
                    Name = "САУ В",
                    ProductSubTypeId = 1
                });
            
            modelBuilder.Entity<StatusType>().HasData(
                new StatusType
                {
                    Id = 1,
                    StatusTypeName = "ПРОЕКТЫ"
                },
                new StatusType
                {
                    Id = 2,
                    StatusTypeName = "ЗАКАЗ"
                });

            modelBuilder.Entity<Status>().HasData(
                new Status
                {
                    Id=1,
                    Name = "В РАБОТЕ",
                    StatusTypeId = 1,
                    IsSystem = true
                },
                new Status
                {
                    Id=2,
                    Name = "ЗАВЕРШЕН",
                    StatusTypeId = 1,
                    IsSystem = true
                },
                new Status
                {
                    Id=3,
                    Name = "В РАБОТЕ",
                    StatusTypeId = 1,
                    IsSystem = true
                },
                new Status
                {
                    Id=4,
                    Name = "ЗАВЕРШЕН",
                    StatusTypeId = 1,
                    IsSystem = true
                });
            
        }
    }
}