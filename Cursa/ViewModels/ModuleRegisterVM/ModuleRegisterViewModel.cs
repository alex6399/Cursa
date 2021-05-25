using System;
using System.ComponentModel.DataAnnotations;
using Cursa.ViewModels.Base;

namespace Cursa.ViewModels.ModuleRegisterVM
{
    public class ModuleRegisterViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Серийный №")]
        public string SerialNum { get; set; }
        [Display(Name = "Зав. №")]
        public string CertifiedNum { get; set; }
        [Display(Name = "Подпроект")]
        public int SubProjectId { get; set; }
        [Display(Name = "Подпроект")]
        public string SubProjectName { get; set; }
        [Display(Name = "Карта заказа")]
        public int OrderCardId { get; set; }
        [Display(Name = "Карта заказа")]
        public string OrderCardName { get; set; }
        [Display(Name = "Отгружено")]
        public DateTime? ShippedDate { get; set; }
    }
}