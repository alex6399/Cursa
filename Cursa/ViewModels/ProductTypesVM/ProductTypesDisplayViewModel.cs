using System.ComponentModel.DataAnnotations;

namespace Cursa.ViewModels.ProductTypesVM
{
    public class ProductTypesDisplayViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        // [Display(Name = "Тип")]
        // public string ProductTypeName { get; set; }
    }
}