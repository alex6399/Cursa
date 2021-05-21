using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class Department : BaseEntity
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Наименование")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Display(Name = "Примечание")]

        public string Description { get; set; }
        [Display(Name = "Дата создания")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "Системный ?")]
        public bool IsSystem { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}