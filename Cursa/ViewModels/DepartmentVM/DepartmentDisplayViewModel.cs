using System.ComponentModel.DataAnnotations;

namespace Cursa.ViewModels.DepartmentVM
{
    public class DepartmentDisplayViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Наименование")] public string Name { get; set; }
    }
}