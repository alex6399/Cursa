using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class ProductSubType : BaseEntity
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Наименование")]
        [MaxLength(40)]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        [MaxLength(1024,ErrorMessage = "Превышен допустимый размер поля")]
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; } 
        public ICollection<ProductType> ProductTypes { get; set; }
    }
}
