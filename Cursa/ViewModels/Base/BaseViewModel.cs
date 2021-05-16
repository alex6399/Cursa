using System.ComponentModel.DataAnnotations;

namespace Cursa.ViewModels.Base
{
    public class BaseViewModel  
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }
    }
}