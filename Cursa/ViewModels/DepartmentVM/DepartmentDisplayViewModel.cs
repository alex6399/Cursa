using System.ComponentModel.DataAnnotations;

namespace Cursa.ViewModels.DepartmentVM
{
    public class DepartmentDisplayViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        [MaxLength(51, ErrorMessage = "Максимальное количество символов 51")]
        public string Name { get; set; }
    }
}