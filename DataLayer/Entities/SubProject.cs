using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class SubProject:BaseEntityTracking
    {
        [Required]
        [Display(Name = "Проект")]
        public int ProjectId { get; set; }
        [Display(Name = "Проект")]
        public virtual Project Project { get; set; }
        [Required] 
        [MaxLength(200)]
        [Display(Name = "Наименование подпроекта")]
        public string Name { get; set; }
        [Required] 
        [MaxLength(100)]
        [Display(Name = "Код")]
        public string Code { get; set; }
        [Display(Name = "Ответственный сотрудник")]
        public int EmployeeId { get; set; }
        [Display(Name = "Ответственный")]
        public virtual Employee Employee { get; set; }
        [Display(Name = "Статус")]
        public int StatusId { get; set; }
        [Display(Name = "Статус")]
        public virtual Status Status { get; set; }
        [Display(Name = "Договор")] 
        public int? ContractId { get; set; }
        [Display(Name = "Договор")] 
        public virtual Contract Contract { get; set; }
        [Display(Name = "Описание")] 
        public string Description { get; set; }
        [Display(Name = "Дата сдачи")]
        public DateTime EndDate { get; set; }
        public virtual ICollection<ContractorSubProject> ContractorSubProjects { get; set; }
    }
}