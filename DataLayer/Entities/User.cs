using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Дата создания")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "Дата последнего изменения")]
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedUserId { get; set; }
        public virtual User CreatedUser { get; set; }
        public int? ModifiedUserId { get; set; }
        public virtual User ModifiedUser { get; set; }
        public bool IsLockout { get; set; }
        public virtual ICollection <Project> CreatedProjects { get; set; }
        public virtual ICollection <Project> ModifiedProjects { get; set; }
        public virtual ICollection <SubProject> CreatedSubProjects { get; set; }
        public virtual ICollection <SubProject> ModifiedSubProjects { get; set; }
        public virtual ICollection <Contract> CreatedContracts { get; set; }
        public virtual ICollection <Contract> ModifiedContracts{ get; set; }
        public virtual ICollection <Employee> CreatedEmployees { get; set; }
        public virtual ICollection <Employee> ModifiedEmployees{ get; set; }
        public virtual ICollection <Module> CreatedModules { get; set; }
        public virtual ICollection <Module> ModifiedModules{ get; set; }
        public virtual ICollection <Product> CreatedProducts { get; set; }
        public virtual ICollection <Product> ModifiedProducts{ get; set; }
        public virtual ICollection <User> CreatedUsers { get; set; }
        public virtual ICollection <User> ModifiedUsers { get; set; }
        
        public string GetFullName => LastName+" "+FirstName[0] + ". " + MiddleName[0] + ".";
    }
}