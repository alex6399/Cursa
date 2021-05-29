using System;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities;

namespace Cursa.ViewModels.ModuleVM
{
    public class ModuleDisplayViewModel
    {
        public int Id { get; set; }
        public int ModuleTypeId { get; set; } // TODO посути тут еще нужно ModuleSupTypes
        [Display(Name = "Модуль")]
        public ModuleType ModuleType { get; set; }
        [Display(Name = "Изготовлен по К.З №")]
        public int DestinationOrderCardId { get; set; }
        [Display(Name = "Карте заказа")]
        public string DestinationOrderCardName { get; set; }
        [Display(Name = "Карте номер")]
        public string DestinationOrderCardNumber { get; set; }
        // public virtual OrderCard DestinationOrderCard { get; set; }
        
        [Display(Name = "Серийный №")]
        public string SerialNumber { get; set; }
        [Display(Name = "№ места")]
        public int Place { get; set; }
        [Display(Name = "Установлен по К.З №")]
        public int? ActualOrderCardId { get; set; }
        [Display(Name = "К.З")]
        public string ActualOrderCardName { get; set; }
        [Display(Name = "К.З")]
        public string ActualOrderCardNumber { get; set; }
        // [Display(Name = "Установлен по карте заказа")]
        // public virtual OrderCard ActualOrderCard { get; set; }
        [Display(Name = "Дата заказа")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "Дата изготовления")]
        public DateTime? ManufacturingData { get; set; }
    }
}