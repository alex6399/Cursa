using System;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Cursa.ViewModels.SubProjectVM
{
    public class SubProjectCreateEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Проект")]
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MaxLength(51, ErrorMessage = "Максимальное количество символов 51")]
        [Display(Name = "Наименование подпроекта")]
        [Remote("IsNameSubProjectExist","SubProjects",
            ErrorMessage = "Подпроект уже существует",
            AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MaxLength(51, ErrorMessage = "Максимальное количество символов 51")]
        [Display(Name = "Код")]
        [RegularExpression(@"((^[0-9]+)|(^П-[0-9]+))((\.)*[0-9])*",ErrorMessage = "Неверный формат")]
        [Remote("IsCodeSubProjectExist","SubProjects",
            ErrorMessage = "Подпроект уже существует",
            AdditionalFields = "Id")]
        public string Code { get; set; }
        [Display(Name = "Ответственный сотрудник")]
        public int EmployeeId { get; set; }
        [Display(Name = "Статус")]
        public int StatusId { get; set; }
        [Display(Name = "Договор №")] 
        public string Contract { get; set; }
        [Display(Name = "Контрагент")] 
        public int? ContractorId { get; set; }
        [Display(Name = "Примечание")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Дата сдачи")]
        public DateTime? EndDate { get; set; }
    }
}