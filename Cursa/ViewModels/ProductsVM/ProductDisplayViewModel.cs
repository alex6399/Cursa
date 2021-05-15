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
        [Display(Name = "Описание")]
        [MaxLength(300,ErrorMessage = "Превышен допустимый лимит символов")]
        public string Description { get; set; }
    }
}