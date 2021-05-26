using System;
using System.ComponentModel.DataAnnotations;
using Cursa.ViewModels.Base;

namespace Cursa.ViewModels.ModuleRegisterVM
{
    public class ModuleRegisterViewModel
    {
        public int Id { get; set; } // 0 (hidden)
        [Display(Name = "Серийный №")]
        public string SerialNumber { get; set; } // 1
        [Display(Name = "Подпроект")]
        public int SubProjectId { get; set; }
        [Display(Name = "Подпроект")]
        public string SubProjectName { get; set; } // 2
        [Display(Name = "Установлен по К.З")]
        public int ActualOrderCardId { get; set; }
        [Display(Name = "Установлен по К.З")]
        public string ActualOrderCardNumber { get; set; } // 3
        [Display(Name = "Изготовлен по К.З")]
        public int DestOrderCardId { get; set; }
        [Display(Name = "Изготовлен по К.З")]
        public string DestOrderCardNumber { get; set; } // 4
        [Display(Name = "Отгружено")]
        public DateTime? ManufacturingData { get; set; } // 5
        [Display(Name = "Модуль")]
        public string ModuleTypeName { get; set; } // 6
        [Display(Name = "Продукт")]

        public int ProductId { get; set; }
        [Display(Name = "Продукт")]
        public string ProductName { get; set; } //7
        [Display(Name = "№")]
        public string ProductNumber { get; set; } //8
    }
}