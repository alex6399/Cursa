using System.ComponentModel.DataAnnotations;
using AutoMapper;
using DataLayer.Entities;

namespace Cursa.ViewModels.EmployeesVM
{
    public class EmployeesViewModel
    {
        public int Id { get; set; }
        [Display(Name = "ФИО")]
        public string FullName { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Display(Name = "Отдел")]
        public string DepartmentName { get; set; }
    }
}