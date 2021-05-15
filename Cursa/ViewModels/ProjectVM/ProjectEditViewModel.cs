using System.ComponentModel.DataAnnotations;

namespace Cursa.ViewModels.ProjectVM
{
    public class ProjectEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Максимальная длина")]
        [Display(Name = "Наименование проекта")]
        public string Name { get; set; }
        [Display(Name = "Владелец")]
        public int? OwnerId { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Код")]
        public string Code { get; set; }
        [Required]
        [Display(Name = "Ответственный")]
        public int EmployeeId { get; set; }
        public string Description { get; set; }
    }
}
