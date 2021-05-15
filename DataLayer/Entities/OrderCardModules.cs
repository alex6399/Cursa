using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    class OrderCardModules
    {
        public int OrderCardId { get; set; }
        public virtual OrderCard OrderCards { get; set; }
        public int ModuleId { get; set; }
        public virtual ModuleType ModulesType { get; set; }
    }
}