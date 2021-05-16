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


            modelBuilder.Entity<ModuleType>().HasData(
                new ModuleType
                {
                    Id = 1,
                    Name = "Модуль ввода аналоговый 4-канальный IA - 4k42 - M ЛДАР.468155.047"
                },
                new ModuleType
                {
                    Id = 2,
                    Name = "Модуль ввода аналоговый 8-канальный IA-8k42 ЛДАР.468155.049"
                },
                new ModuleType
                {
                    Id = 3,
                    Name = "Модуль вывода аналоговый 4-канальный OA - 4k42 - M ЛДАР.468155.046"
                },
                new ModuleType
                {
                    Id = 4,
                    Name = "Модуль ввода дискретный 8 - канальный ID - 8k24 - M ЛДАР.469219.043"
                },
                new ModuleType
                {
                    Id = 5,
                    Name = "Модуль ввода дискретный 16 - канальный ID - 16k24 ЛДАР.469219.060"
                },
                new ModuleType
                {
                    Id = 6,
                    Name = "Модуль вывода дискретный 5 - канальный OD - 5k - M ЛДАР.468154.050"
                },
                new ModuleType
                {
                    Id = 7,
                    Name = "Модуль вывода дискретный 16 - канальный ОD - 16k24 ЛДАР.468154.055"
                },
                new ModuleType
                {
                    Id = 8,
                    Name = "Модуль измерения частоты 3 - канальный IF - 3k ЛДАР.468155.048"
                },
                new ModuleType
                {
                    Id = 9,
                    Name = "Модуль адаптера USB/RS-485-4k ЛДАР.469239.104"
                },
                new ModuleType
                {
                    Id = 10,
                    Name = "Модуль адаптера Com/RS-485 ЛДАР.469239.299"
                },
                new ModuleType
                {
                    Id = 11,
                    Name = "Блок системный LPBS-15-М ЛДАР.469239.235"
                },
                new ModuleType
                {
                    Id = 12,
                    Name = "Блок системный LPBS-7-М ЛДАР.469239.256"
                },
                new ModuleType
                {
                    Id = 13,
                    Name = "Комплект ПО \"ЭЛАР - ПРО\""
                },
                new ModuleType
                {
                    Id = 14,
                    Name = "Исполнительная система \"M - PLC\""
                }
            );

            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    Name = "Конструкторский"
                    
                },
                new Department
                {
                    Id = 2,
                    Name = "Управление"
                }
            );


            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id=1,
                    FirstName = "Александр",
                    MiddleName = "Сергеевич",
                    LastName = "Хватов",
                    Phone = "+79173155974",
                    DepartmentId = 1
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Сергей",
                    MiddleName = "Сергеевич",
                    LastName = "Хватов",
                    Phone = "+79173155975",
                    DepartmentId = 1
                },
                new Employee
                {
                    Id = 3,
                    FirstName = "Олег",
                    MiddleName = "Сергеевич",
                    LastName = "Хватов",
                    Phone = "+79173155976",
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
            
            modelBuilder.Entity<SystemUnitType>().HasData(
                new SystemUnitType
                {
                    Id = 1,
                    Name = " LPBS-15-М",
                    MaxNumberModule = 15
                });
            
            modelBuilder.Entity<SystemUnit>().HasData(
                new SystemUnit
                {
                    Id = 1,
                    Name = "LPBS",
                    SystemUnitTypeId = 1,
                    MaxNumberModule = 15
                });
            modelBuilder.Entity<Contract>().HasData(
                new Contract
                {
                    Id = 1,
                    Name = "№: ТО -084Т18"
                },
                new Contract
                {
                    Id = 2,
                    Name = "№: 20/16-КР-2016-СПб-1"
                },
                new Contract
                {
                    Id = 3,
                    Name = "№: 77П-ТК/12/16"
                });

            //modelBuilder.Entity<Status>.HasData(new Status {Id=1, NameStatus = "В РАБОТЕ", StatusTypeId = 1,StatusType = new StatusType()});
            //modelBuilder.Entity<Status>.HasData(new Status {Id=1, NameStatus = "В РАБОТЕ", StatusTypeId = 1,StatusType = new StatusType()});

            //modelBuilder.Entity<Book>().HasData(
            //    new Book { BookId = 1, AuthorId = 1, Title = "Hamlet" },
            //    new Book { BookId = 2, AuthorId = 1, Title = "King Lear" },
            //    new Book { BookId = 3, AuthorId = 1, Title = "Othello" }
            //);
        }
    }
}