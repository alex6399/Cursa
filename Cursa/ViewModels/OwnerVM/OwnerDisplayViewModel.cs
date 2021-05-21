using System.ComponentModel.DataAnnotations;

namespace Cursa.ViewModels.OwnerVM
{
    public class OwnerDisplayViewModel
    {
        public int Id { get; set; }
        [Display(Name="Отдел")]
        public string Name { get; set; }
    }
}