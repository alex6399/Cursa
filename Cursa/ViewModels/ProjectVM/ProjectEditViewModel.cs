using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Cursa.ViewModels.ProjectVM
{
    public class ProjectEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MaxLength(100, ErrorMessage = "Максимальная длина")]
        [Display(Name = "Наименование проекта")]
        [Remote("IsNameProjectExist","Projects",
            ErrorMessage = "Проект уже существует",
            AdditionalFields = "Id")]
        public string Name { get; set; }
        [Display(Name = "Владелец")]
        public int? OwnerId { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MaxLength(100)]
        [Display(Name = "Код")]
        [RemoteAttribute("IsCodeProjectExist","Projects",
            ErrorMessage = "Проект с таким кодом уже существует",
            AdditionalFields = "Id")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Ответственный")]
        public int EmployeeId { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
