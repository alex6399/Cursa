using System.ComponentModel.DataAnnotations;

namespace Cursa.ViewModels.ContractorsVM
{
    public class ContractorsDisplayViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Наименование")] public string Name { get; set; }
    }
}