using System.Collections.Generic;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class Contract:BaseEntityTracking
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<SubProject> SubProjects { get; set; }
    }
}