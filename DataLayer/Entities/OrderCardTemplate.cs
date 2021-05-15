using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    class OrderCardTemplate:BaseEntityTracking
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Наименование")]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Path { get; set; }// нужен ли он сейчас
        [Display(Name = "Продукт")]
        public int ProductTypeId { get; set; }
        [Display(Name = "Продукт")]
        public ProductType ProductTypeduct { get; set; }
    }
}
