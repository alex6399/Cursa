using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class Module : BaseEntityTracking
    {
        public int ModuleTypeId { get; set; } // TODO посути тут еще нужно ModuleSupTypes
        [Display(Name = "Модуль")]
        public ModuleType ModuleType { get; set; }
        [Display(Name = "Изготовлен по карте заказа")]
        public int DestinationOrderCardId { get; set; }
        [Display(Name = "Изготовлен по карте заказа")]
        public virtual OrderCard DestinationOrderCard { get; set; }
        
        [Display(Name = "Серийный №")]
        public string SerialNumber { get; set; }
        [Display(Name = "№ места")]
        public int Place { get; set; }
        [Display(Name = "Установлен по карте заказа")]
        public int? ActualOrderCardId { get; set; }
        [Display(Name = "Установлен по карте заказа")]
        public virtual OrderCard ActualOrderCard { get; set; }
        [Display(Name = "Дата изготовления")]
        public DateTime? ManufacturingData { get; set; }
        // public virtual ICollection<OrderCardModules> OrderCardModulesCollection { get; set; }
    }
}