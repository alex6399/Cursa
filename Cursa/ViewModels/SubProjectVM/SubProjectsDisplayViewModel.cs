using System;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities;

namespace Cursa.ViewModels.SubProjectVM
{
    public class SubProjectsDisplayViewModel
    {
        public int Id { get; set; }
        // [Display(Name = "Проект")] public string ProjectName { get; set; }

        [Display(Name = "Наименование подпроекта")]
        public string Name { get; set; }

        [Display(Name = "Код")] public string Code { get; set; }

        [Display(Name = "Ответственный сотрудник")]
        public EmployeePartDisplayViewModel Employee { get; set; }

        [Display(Name = "Статус")] public string StatusName { get; set; }
        [Display(Name = "Договор №")] public string Contract { get; set; }
        // [Display(Name = "Описание")] public string Description { get; set; }
        [Display(Name = "Создан")] public DateTime? CreatedDate { get; set; }
        [Display(Name = "Сдача")] public DateTime? EndDate { get; set; }
    }

    public class EmployeePartDisplayViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Ответственный")] public string FullName { get; set; }
    }

    // public class ContractPartDisplayViewModel
    // {
    //     public int Id { get; set; }
    //     [Display(Name = "Договор")] public string Name { get; set; }
    // }
}