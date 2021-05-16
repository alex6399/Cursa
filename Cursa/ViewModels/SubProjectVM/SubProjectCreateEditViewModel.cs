using System;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities;

namespace Cursa.ViewModels.SubProjectVM
{
    public class SubProjectCreateEditViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Проект")]
        public int ProjectId { get; set; }
        [Required] 
        [MaxLength(100,ErrorMessage = "Превышен лимит символов")]
        [Display(Name = "Наименование подпроекта")]
        public string Name { get; set; }
        [Required] 
        [MaxLength(100,ErrorMessage = "Превышен лимит символов")]
        [Display(Name = "Код")]
        public string Code { get; set; }
        [Display(Name = "Ответственный сотрудник")]
        public int EmployeeId { get; set; }
        [Display(Name = "Статус")]
        public int StatusId { get; set; }
        [Display(Name = "Договор")] 
        public int? ContractId { get; set; }
        [Display(Name = "Контрагент")] 
        public int? ContractorId { get; set; }
        [Display(Name = "Примечание")]
        public string Description { get; set; }
        [Display(Name = "Дата сдачи")]
        public DateTime EndDate { get; set; }
    }
}