using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cursa.ViewModels.OrderCardVM;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Cursa.ViewModels.ModuleVM
{
    public class ModuleCreateEditViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Модуль")]
        public int ModuleTypeId { get; set; }
        [Display(Name = "Модуль")]
        public string ModuleTypeName { get; set; }
        [Display(Name = "Изготовлен по карте заказа")]
        public int DestinationOrderCardId { get; set; }
        [Display(Name = "Изготовлен по карте заказа")]
        public string DestinationOrderCardName { get; set; }
        public string DestinationOrderCardNumber { get; set; }
        
        [Display(Name = "Серийный №")]
        public string SerialNumber { get; set; }
        [Display(Name = "№ места")]
        public int Place { get; set; }
        [Display(Name = "Версия прошивки")]
        public string FirmwareVersion { get; set; }
        [Display(Name = "Установил")]
        public int? EmployeeId { get; set; }
        // [Display(Name = "Установил")]
        // public Employee Employee { get; set; }
        [Display(Name = "Установлен по карте заказа")]
        public int? ActualOrderCardId { get; set; }
        [Display(Name = "Дата изготовления")]
        public DateTime? ManufacturingData { get; set; }



        public int ProjectId { get; set; }
        public int SubProjectId { get; set; }
        public int ProductId { get; set; }
        //public int ActualOrderCardId { get; set; }
    }
    
}