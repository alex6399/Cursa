using System.ComponentModel.DataAnnotations;
using Cursa.ViewModels.Base;

namespace Cursa.ViewModels.OrderCardVM
{
    public class OrderCardDisplayViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Наименование")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Номер карты заказа")]
        [MaxLength(100)]
        public string Number { get; set; }
        [Display(Name = "Подпроект")]
        public BaseViewModel Product { get; set; }
        [Display(Name = "Продукция")]
        public string ProductName { get; set; }
        [Display(Name = "Ответственный")]

        public string EmployeeName { get; set; }
    }
}