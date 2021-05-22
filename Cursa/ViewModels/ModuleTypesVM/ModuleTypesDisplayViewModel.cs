using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cursa.ViewModels.ModuleTypesVM
{
    public class ModuleTypesDisplayViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MaxLength(120, ErrorMessage = "Максимальное количество 120 символов")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MaxLength(100)]
        [Display(Name = "Код")]
        public string Code { get; set; }

        [Display(Name = "УСО ?")] public bool IsCommunicationDevice { get; set; }

        [Display(Name = "Колличество подключаемых УСО")]
        public int NumberConnectionPoints { get; set; }

        [Display(Name = "Количество каналов")] public int CountChanel { get; set; }
        [Display(Name = "Действующий ?")] public bool IsActiv { get; set; }
    }
}