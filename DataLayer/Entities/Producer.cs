using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Producer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Примечание")] public string Description { get; set; }
        [Display(Name = "Дата создания")] public DateTime CreatedDate { get; set; }
    }
}