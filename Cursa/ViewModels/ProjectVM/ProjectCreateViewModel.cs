using System.ComponentModel.DataAnnotations;
using DataLayer.Entities;


namespace Cursa.ViewModels.ProjectVM
{
    public class ProjectCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MaxLength(100, ErrorMessage = "Максимальная длина")]
        [Display(Name = "Наименование проекта")]
        public string Name { get; set; }
        [Display(Name = "Владелец")]
        public int? OwnerId { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MaxLength(100)]
        [Display(Name = "Код")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Ответственный")]
        public int EmployeeId { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
