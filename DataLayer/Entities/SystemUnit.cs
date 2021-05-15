using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
   public class SystemUnit:BaseEntity // Системный блок
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Серийный номер")]
        public string? SerialNum { get; set; }

        public int SystemUnitTypeId { get; set; }
        public SystemUnitType SystemUnitType { get; set; }
        public uint MaxNumberModule { get; set; }// нужно ли дублировать с типа SystemUnitType
        public uint CurrentNumberModule { get; set; }
        [Display(Name = "Дата создания")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Дата прибытия")]
        public DateTime ArrivalDate { get; set; }
    }
}
