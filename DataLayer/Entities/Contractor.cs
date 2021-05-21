using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class Contractor:BaseEntity
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MaxLength(40, ErrorMessage = "Максимальное количество символов 40")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [MaxLength(256, ErrorMessage = "Максимальное количество символов 256")]
        [Display(Name = "Примечание")]
        public string Description { get; set; }
        [Display(Name = "Дата создания")]
        public DateTime? CreatedDate { get; set; }
        public virtual ICollection<SubProject> SubProjects{ get; set; }
    }
}