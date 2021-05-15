using System;
using System.ComponentModel.DataAnnotations;
using Cursa.ViewModels.Base;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cursa.ViewModels.ProductsVM
{
    public class ProductCreateViewModel:BaseViewModel
    {
        [Display(Name = "Серийный номер")]
        public string SerialNum { get; set; }
        [Display(Name = "Зав. номер")]
        public string CertifiedNum { get; set; }
        [Display(Name = "Тип")]
        public int ProductTypeId { get; set; }
        [Display(Name = "Тип")]
        public virtual ProductType ProductType { get; set; }
        [Display(Name = "Подпроект")]
        public int SubProjectId { get; set; }
        [Display(Name = "Подпроект")]
        public SubProject SubProject { get; set; }
        [Display(Name = "Описание")]
        [MaxLength(300,ErrorMessage = "Превышен допустимый лимит символов")]
        public string Description { get; set; }
        [Display(Name = "Сформирован")]
        public bool IsFormed { get; set; } // сформирован?
        public DateTime? ManufacturingDate { get; set; }// дата изготовления
        public DateTime? ShippedDate { get; set; }// дата отгрузки

        /*// поля со значения для выпадающих списков
        public SelectList Subprojects { get; set; }
        
        public SelectList ProductTypes { get; set; }
        */
    }
}