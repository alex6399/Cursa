using System.ComponentModel.DataAnnotations;

namespace Cursa.ViewModels.StatusVM
{
    public class StatusDisplayViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Статус")]
        public string Name { get; set; }
        [Display(Name = "Категория")]
        public string StatusTypeName { get; set; }
    }
}