using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class ModulesLocationsHistory
    {
        public int Id { get; set; }
        public int SubProjectId { get; set; }
        public virtual SubProject SubProject { get; set; }
        public string Description { get; set; }
        public DateTime InstallationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        [Required] 
        public  int InstanceModulesId { get; set; }
        public virtual Module Module { get; set; }
    }
}