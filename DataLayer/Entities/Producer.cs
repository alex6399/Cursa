using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Producer
    {
        public int Id { get; set; }

        [Required] [MaxLength(100)] 
        public string Name { get; set; }

        public string Description { get; set; } 
        public DateTime CreatedDate { get; set; }
    }
}