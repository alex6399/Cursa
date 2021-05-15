using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class Status:BaseEntity
    {
        [Required]
        [MaxLength(40)] 
        public string NameStatus { get; set; }
        public int StatusTypeId { get; set; }
        public StatusType StatusType { get; set; }
        public DateTime CreatedDate { get; set; }
        //public virtual ICollection<ExternalApplication> ExternalApplications { get; set; }
        public virtual ICollection<SubProject> SubProjects { get; set; }
        public virtual ICollection<OrderEmployee> OrderEmployee { get; set; }
    }
}