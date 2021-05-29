using System.ComponentModel.DataAnnotations;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Cursa.ViewModels.ProjectVM
{
    public class ProjectCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MaxLength(100, ErrorMessage = "Максимальная длина")]
        [Display(Name = "Наименование проекта")]
        [RemoteAttribute("IsNameProjectExist","Projects",ErrorMessage = "Проект уже существует")]
        public string Name { get; set; }
        [Display(Name = "Владелец")]
        public int? OwnerId { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MaxLength(100)]
        [RegularExpression(@"^[0-9]+",ErrorMessage = "Неверный формат")]
        [Display(Name = "Код")]
        [RemoteAttribute("IsCodeProjectExist","Projects",ErrorMessage = "Проект с таким кодом уже существует")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Ответственный")]
        public int EmployeeId { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
