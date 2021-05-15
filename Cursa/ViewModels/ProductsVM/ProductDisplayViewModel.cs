using System;
using System.ComponentModel.DataAnnotations;
using Cursa.ViewModels.Base;
using DataLayer.Entities;

namespace Cursa.ViewModels.ProductsVM
{
    public class ProductDisplayViewModel:BaseViewModel
    {
        [Display(Name = "Серийный номер")]
        public string SerialNum { get; set; }
        [Display(Name = "Зав. номер")]
        public string CertifiedNum { get; set; }
        [Display(Name = "Подпроект")]
        public BaseViewModel SubProject { get; set; }
        [Display(Name = "Заказано")]
        public DateTime? OrderDate { get; set; }
        [Display(Name = "Изготовлено")]
        public DateTime? ManufacturingDate { get; set; }
        [Display(Name = "Отгружено")]
        public DateTime? ShippedDate { get; set; }
    }
}