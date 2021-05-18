using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cursa.ViewModels.AccountVM
{
    public class ChangePasswordViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Имя")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Новый пароль")]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 8)]
        public string NewPassword { get; set; }

    }
}
