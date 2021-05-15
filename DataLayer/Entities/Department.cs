using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class Department:BaseEntity
    {
        [Required] [MaxLength(50)] 
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}