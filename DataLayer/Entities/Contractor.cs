using System;
using System.Collections.Generic;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class Contractor:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<ContractorSubProject> ContractorSubProjects{ get; set; }
    }
}