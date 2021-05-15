using System;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class ProductType:BaseEntity
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Наименование")]
        [MaxLength(40)]
        public string Name { get; set; }
        [Display(Name = "Тип")]
        public int ProductSubTypeId { get; set; }
        [Display(Name = "Тип")]
        public ProductSubType ProductSubType { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}