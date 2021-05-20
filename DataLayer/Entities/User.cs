using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.Entities
{
    public class User : IdentityUser<int>
    {
        [Display(Name = "Имя")] public string FirstName { get; set; }
        [Display(Name = "Отчество")] public string MiddleName { get; set; }
        [Display(Name = "Фамилия")] public string LastName { get; set; }
        [Display(Name = "Дата создания")] public DateTime? CreatedDate { get; set; }

        [Display(Name = "Дата последнего изменения")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Зарегистрировал")] public int? CreatedUserId { get; set; }
        [Display(Name = "Зарегистрировал")] public virtual User CreatedUser { get; set; }

        [Display(Name = "Автор последних изменений")]
        public int? ModifiedUserId { get; set; }

        [Display(Name = "Автор последних изменений")]
        public virtual User ModifiedUser { get; set; }

        [Display(Name = "Сменить пароль ?")] public bool IsPasswordChange { get; set; }
        [Display(Name = "Заблокирован ?")] public bool IsBanned { get; set; }
        [Display(Name = "Системный ?")] public bool IsSystem { get; set; }

        public virtual ICollection<Project> CreatedProjects { get; set; }
        public virtual ICollection<Project> ModifiedProjects { get; set; }
        public virtual ICollection<SubProject> CreatedSubProjects { get; set; }
        public virtual ICollection<SubProject> ModifiedSubProjects { get; set; }
        public virtual ICollection<Employee> CreatedEmployees { get; set; }
        public virtual ICollection<Employee> ModifiedEmployees { get; set; }
        public virtual ICollection<Module> CreatedModules { get; set; }
        public virtual ICollection<Module> ModifiedModules { get; set; }
        public virtual ICollection<Product> CreatedProducts { get; set; }
        public virtual ICollection<Product> ModifiedProducts { get; set; }
        public virtual ICollection<User> CreatedUsers { get; set; }
        public virtual ICollection<User> ModifiedUsers { get; set; }

        public string GetFullName => LastName + " " + FirstName + " " + MiddleName;
    }
}