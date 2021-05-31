using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities
{
    [Index(nameof(Code), IsUnique = true)]
    public class Project:BaseEntityTracking
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MaxLength(100, ErrorMessage = "Максимальная длина")]
        [Display(Name = "Наименование проекта")]
        public string Name { get; set; }
        [Display(Name = "Владелец")]
        public int? OwnerId { get; set; }
        [Display(Name = "Владелец")]
        public virtual Owner Owner { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MaxLength(100)]
        [Display(Name = "Код")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Ответственный")]
        public int EmployeeId { get; set; }
        [Display(Name = "Ответственный")]
        public virtual Employee Employee { get; set; }
        [Display(Name = "Примечание")]
        [MaxLength(160, ErrorMessage = "Максимальное количество символов 160")]
        public string Description { get; set; }
        public virtual ICollection<SubProject> SubProjects { get; set; }
    }
}