using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class Status:BaseEntity
    {
        public string Name { get; set; }
        public int StatusTypeId { get; set; }
        public StatusType StatusType { get; set; }
        public bool IsSystem { get; set; }
        public virtual ICollection<SubProject> SubProjects { get; set; }
    }
}