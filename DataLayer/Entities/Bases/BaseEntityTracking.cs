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
        [Display(Name = "Дата последнего изменения")]
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedUserId { get; set; }
        public virtual User CreatedUser { get; set; }
        public int? ModifiedUserId { get; set; }
        public virtual User ModifiedUser { get; set; }
    }
}