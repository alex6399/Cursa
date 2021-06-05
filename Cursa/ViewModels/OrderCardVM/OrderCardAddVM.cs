using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cursa.ViewModels.OrderCardVM
{
    public class OrderCardAddVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Наименование")]
        [MaxLength(51, ErrorMessage = "Максимальное количество символов 51")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Номер карты заказа")]
        [MaxLength(51, ErrorMessage = "Максимальное количество символов 51")]
        public string Number { get; set; }

        public int ProductId { get; set; }

        public List<OrderCardAddModuleVM> Modules { get; set; }
    }

    public class OrderCardAddModuleVM
    {
        public int Id { get; set; } // Id TypeModule

        public string Name { get; set; }
        
        // [Required(ErrorMessage = "Поле обязательно для заполнения")]
        // [Display(Name = "Количество")]
        // //[MaxLength(100)]
        // public int Count { get; set; }

        public bool[] Addresses { get; set; } = new bool[15];

        // [Required(ErrorMessage = "Поле обязательно для заполнения")]
        // [Display(Name = "Наименование")]
        // [MaxLength(100)]
        // public string Name { get; set; }
        // public int ProductId { get; set; }
    }
}