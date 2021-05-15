using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class ContractorSubProject
    {
        public int SubProjectId { get; set; }
        public virtual SubProject SubProject { get; set; }
        public int ContractorId { get; set; }
        public virtual Contractor Contractor { get; set; }
    }
}