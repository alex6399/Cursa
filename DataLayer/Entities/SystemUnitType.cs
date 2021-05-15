using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
   public class SystemUnitType:BaseEntity
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Количество мест под модули")]
        public uint MaxNumberModule { get; set; }
        [Display(Name = "Описание")]
        [MaxLength(1024, ErrorMessage = "Превышен допустимый размер поля")]
        public string Description { get; set; }
    }
}
