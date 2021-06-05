using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cursa.ViewModels.Base;
using Microsoft.AspNetCore.Mvc;

namespace Cursa.ViewModels.OrderCardVM
{
    public class OrderCardCreateEditVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Наименование")]
        [MaxLength(51, ErrorMessage = "Максимальное количество символов 51")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Номер карты заказа")]
        [MaxLength(51, ErrorMessage = "Максимальное количество символов 51")]
        [RegularExpression(@"^[0-9]+(([0-9]*$)|(-у$))",ErrorMessage = "Неверный формат")]
        [Remote("IsNumberOrderCardExist","OrderCards",
            ErrorMessage = "Номер уже существует",
            AdditionalFields = "Id")]
        public string Number { get; set; }

        public BaseViewModel systemUnit { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "Ответственный сотрудник")]
        public int EmployeeId { get; set; }
        
        public List<OrderCardCreateEditModuleVM> ModulesVM { get; set; }
    }
    
    
    public class OrderCardCreateEditModuleVM
    {
        public int Id { get; set; } // Id TypeModule

        public string Name { get; set; }
        public bool[] Addresses { get; set; }
    }
}
