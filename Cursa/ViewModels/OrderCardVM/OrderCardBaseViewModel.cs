using System.ComponentModel.DataAnnotations;

namespace Cursa.ViewModels.OrderCardVM
{
    public class OrderCardBaseViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Наименование")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Номер К.З")]
        [MaxLength(100)]
        public string Number { get; set; }
    }
}