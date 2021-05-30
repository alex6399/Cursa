using System.ComponentModel.DataAnnotations;
using DataLayer.Entities;
using FluentValidation;

namespace Cursa.ViewModels.EmployeesVM
{
    public class EmployeeCreateEditViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Имя")] public string FirstName { get; set; }
        [Display(Name = "Отчество")] public string MiddleName { get; set; }
        [Display(Name = "Фамилия")] public string LastName { get; set; }
        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Phone { get; set; }
        [Display(Name = "Отдел")] public int DepartmentId { get; set; }
    }
}