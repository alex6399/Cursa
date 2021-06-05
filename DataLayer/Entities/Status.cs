using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class Status:BaseEntity
    {
        [Display(Name = "Наименование")]
        [MaxLength(51, ErrorMessage = "Максимальное количество символов 51")]
        public string Name { get; set; }
        [Display(Name = "Системный")]
        public bool IsSystem { get; set; }
        public virtual ICollection<SubProject> SubProjects { get; set; }
    }
}