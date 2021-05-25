using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cursa.ViewModels.OrderCardVM;
using DataLayer.Entities;

namespace Cursa.ViewModels.ModuleVM
{
    public class ModuleCreateEditViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Модуль")]
        public int ModuleTypeId { get; set; } // TODO посути тут еще нужно ModuleSupTypes
        [Display(Name = "Модуль")]
        public string ModuleTypeName { get; set; }
        [Display(Name = "Установлен по К.З №")]
        public OrderCardBaseViewModel ActualOrderCard { get; set; }
        [Display(Name = "Серийный №")]
        public string SerialNumber { get; set; }
        [Display(Name = "№ места")]
        public int Place { get; set; }
        // [Display(Name = "Изготовлен по К.З №")]
        // public OrderCardBaseViewModel DestinationOrderCard { get; set; }
    }

    public class ModuleComplexCreateEditViewModel
    {
        [Display(Name = "Изготовлен по К.З №")]
        public OrderCardBaseViewModel DestinationOrderCard { get; set; }

        [Display(Name = "Дата изготовления")]
        public DateTime? ManufacturingData { get; set; }

        public List<ModuleCreateEditViewModel> Modules { get; set; }
    }
}