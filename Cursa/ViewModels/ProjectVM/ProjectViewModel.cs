using System.ComponentModel.DataAnnotations;

namespace Cursa.ViewModels.ProjectVM
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Наименование проекта")]
        public string Name { get; set; }
        [Display(Name = "Владелец")]
        public string Owner { get; set; }
        [Display(Name = "Код")]
        public string Code { get; set; }
        [Display(Name = "Ответственный")]
        public string Employee { get; set; }

    }
}
