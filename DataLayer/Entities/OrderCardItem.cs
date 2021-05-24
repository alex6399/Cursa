using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class OrderCardItem
    {
        [Display(Name="К.З")]
        public int OrderCardId { get; set; }
        [Display(Name="К.З")]
        public OrderCard OrderCard { get; set; }
        [Display(Name="Тип модуля")]
        public int ModuleTypeId { get; set; }
        [Display(Name="Тип модуля")]
        public ModuleType ModuleType { get; set; }

        [Display(Name = "Адрес")]
        [Column(TypeName = "jsonb")]
        public List<int> Addresses { get; set; }
    }
}