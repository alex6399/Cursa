using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities.Bases;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataLayer.Entities
{
    public class Module : BaseEntityTracking
    {
        [Display(Name = "Модуль")] public int ModuleTypeId { get; set; }
        [Display(Name = "Модуль")] public ModuleType ModuleType { get; set; }
        [Display(Name = "Изготовлен по карте заказа")]
        public int DestinationOrderCardId { get; set; }
        [Display(Name = "Изготовлен по карте заказа")]
        public virtual OrderCard DestinationOrderCard { get; set; }
        [Display(Name = "Серийный №")] 
        [MaxLength(51, ErrorMessage = "Максимальное количество символов 51")]
        public string SerialNumber { get; set; }
        [Display(Name = "№ места")] public int DestinationPlace { get; set; }
        [Display(Name = "Версия прошивки")] [MaxLength(101, ErrorMessage = "Максимальное количество символов 101")]public string FirmwareVersion { get; set; }
        [Display(Name = "Установил")] public int? EmployeeId { get; set; }
        [Display(Name = "Установил")] public Employee Employee { get; set; }
        [Display(Name = "Установлен по карте заказа")]
        public int? ActualOrderCardId { get; set; }
        [Display(Name = "Установлен по карте заказа")]
        public virtual OrderCard ActualOrderCard { get; set; }
        [Display(Name = "№ места")] public int? ActualPlace { get; set; }
        [Display(Name = "Шкаф")] public int? ProductId { get; set; }
        [Display(Name = "Шкаф")] public Product Product { get; set; }
        [Display(Name = "Дата изготовления")] public DateTime? ManufacturingData { get; set; }
    }
}