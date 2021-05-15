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
        public string Name { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}