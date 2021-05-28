using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class StatusType:BaseEntity
    {
        [Required]
        [MaxLength(20,ErrorMessage = "Максимальная длина 20 символов")]
        public string StatusTypeName { get; set; }
        // public DateTime? CreatedDate { get; set; }

        public ICollection<Status> Statuses{ get; set; }
    }
}
