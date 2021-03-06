using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class OrderCard : BaseEntityTracking
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Наименование")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Номер карты заказа")]
        [MaxLength(100)]
        public string Number { get; set; }

        [Display(Name = "Прикрепить")] public string Path { get; set; } // нужен ли он сейчас
        [Display(Name = "Продукт")] public int ProductId { get; set; }
        [Display(Name = "Продукт")] public Product Product { get; set; }

        // [Display(Name = "Дата завершения")] public DateTime? EndDate { get; set; }

        [Display(Name = "Ответственный сотрудник")]
        public int EmployeeId { get; set; }

        [Display(Name = "Ответственный")] 
        public virtual Employee Employee { get; set; }

        // Start : Json поппытка
        [Column(TypeName = "jsonb")] 
        public List<int> UnSelectedModuleTypes { get; set; }
        // End : Json поппытка

        public virtual ICollection<Module> Modules { get; set; }
        public virtual ICollection<OrderCardItem> OrderCardItems { get; set; }
    }
}