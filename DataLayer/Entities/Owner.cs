using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name="Отдел")]
        public string Name { get; set; }
        // [Display(Name="Дата создания")]
        // public DateTime? CreatedDate { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
       
    }
}