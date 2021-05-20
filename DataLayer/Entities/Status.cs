using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class Status:BaseEntity
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Статус")]
        [MaxLength(40)]
        public string Name { get; set; }
        [Display(Name = "Категория")]
        public int StatusTypeId { get; set; }
        [Display(Name = "Категория")]
        public StatusType StatusType { get; set; }
        [Display(Name = "Дата создания")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Системный?")]
        public bool IsSystem { get; set; }
        public virtual ICollection<SubProject> SubProjects { get; set; }
    }
}