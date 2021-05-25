using System;
using System.ComponentModel.DataAnnotations;
using Cursa.ViewModels.SubProjectVM;

namespace Cursa.ViewModels.ProjectRegisterVM
{
    public class ProjectRegisterDisplayViewModel
    {
        public int Id { get; set; } // SubProjectId
        // [Display(Name = "Проект")] public string ProjectName { get; set; }
        public int ProjectId { get; set; }
        [Display(Name = "Проект")]
        public string ProjectName { get; set; }

        [Display(Name = "Подпроект")]
        public string Name { get; set; }

        [Display(Name = "Код")] public string Code { get; set; }

        [Display(Name = "Ответственный сотрудник")]
        public EmployeePartDisplayViewModel Employee { get; set; }

        [Display(Name = "Статус")] public string StatusName { get; set; }
        [Display(Name = "Договор №")] public string Contract { get; set; }
        [Display(Name = "Контрагент")] public string Contractor { get; set; }
        [Display(Name = "Владелец")] public string Owner { get; set; }
        // [Display(Name = "Описание")] public string Description { get; set; }
        [Display(Name = "Создан")] public DateTime? CreatedDate { get; set; }
        [Display(Name = "Сдача")] public DateTime? EndDate { get; set; }
    }
}