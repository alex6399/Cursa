using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class Employee : BaseEntityTracking
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Имя")]
        [MaxLength(256, ErrorMessage = "Максимальное количество символов 256")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Отчество")]
        [MaxLength(256, ErrorMessage = "Максимальное количество символов 256")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Фамилия")]
        [MaxLength(256, ErrorMessage = "Максимальное количество символов 256")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Неверный формат")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Отдел")] public int DepartmentId { get; set; }
        [Display(Name = "Отдел")] public Department Department { get; set; }
        public virtual ICollection<SubProject> SubProjects { get; set; }
        public string GetFullName => LastName + " " + FirstName + " " + MiddleName;
    }
}