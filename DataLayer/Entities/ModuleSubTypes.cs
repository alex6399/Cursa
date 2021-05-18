using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class ModuleSubTypes
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [MaxLength(40, ErrorMessage = "Превышен лимит символов: 40")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }
    }
}