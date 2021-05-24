using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cursa.ViewModels.Base;

namespace Cursa.ViewModels.OrderCardVM
{
    public class OrderCardCreateEditVM
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

        public BaseViewModel systemUnit { get; set; }
        public int ProductId { get; set; }
        
        public List<OrderCardCreateEditModuleVM> ModulesVM { get; set; }
    }
    
    
    public class OrderCardCreateEditModuleVM
    {
        public int Id { get; set; } // Id TypeModule

        public string Name { get; set; }
        public bool[] Addresses { get; set; }
    }
}
