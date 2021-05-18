using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Bases
{
    public abstract class BaseEntityTracking:BaseEntity
    {
        [Display(Name = "Дата создания")]
        public DateTime? CreatedDate { get; set; }
        
        [Display(Name = "Создатель")]
        public int? CreatedUserId { get; set; }
        [Display(Name = "Создатель")]
        public virtual User CreatedUser { get; set; }
        [Display(Name = "Дата последнего изменения")]
        public DateTime? ModifiedDate { get; set; }
        [Display(Name = "Автор последних изменений")]
        public int? ModifiedUserId { get; set; }
        [Display(Name = "Автор последних изменений")]
        public virtual User ModifiedUser { get; set; }
    }
}