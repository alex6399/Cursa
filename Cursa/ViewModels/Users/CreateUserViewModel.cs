using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cursa.ViewModels.Users
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Имя")]
        [MaxLength(51, ErrorMessage = "Максимальное количество символов 51")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Отчество")]
        [MaxLength(51, ErrorMessage = "Максимальное количество символов 51")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Фамилия")]
        [MaxLength(51, ErrorMessage = "Максимальное количество символов 51")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MaxLength(21, ErrorMessage = "Максимальное количество символов 21")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Неверный формат")]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}