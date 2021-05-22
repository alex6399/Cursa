using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    [Table("ModuleTypes")]
    public class ModuleType : BaseEntity
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MaxLength(120, ErrorMessage = "Максимальное количество 120 символов")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [MaxLength(100)]
        [Display(Name = "Код")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Количество каналов")]
        public int CountChanel { get; set; }
        [Display(Name = "Действующий")] public bool IsActiv { get; set; }
        [Display(Name = "УСО")] public bool IsCommunicationDevice { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Колличество подключаемых УСО")]
        public int NumberConnectionPoints { get; set; }
        // [Display(Name = "Системный ?")] public bool IsSystem { get; set; }
        [MaxLength(160, ErrorMessage = "Максимальное количество 160 символов")]
        [Display(Name = "Примечание")]
        public string Description { get; set; }
        [Display(Name = "Дата создания")] public DateTime? CreatedDate { get; set; }
        public virtual ICollection<Module> Modules{ get; set; }
    }
}