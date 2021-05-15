using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class ModuleType:BaseEntity
    {
        [MaxLength(120,ErrorMessage = "Максимальное количество 120 символов")]
        public string Name { get; set; }
        [MaxLength(1200, ErrorMessage = "Максимальное количество 1200 символов")]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}