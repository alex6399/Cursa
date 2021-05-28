using System;
using System.ComponentModel.DataAnnotations;
using Cursa.ViewModels.Base;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cursa.ViewModels.ProductsVM
{
    public class ProductCreateViewModel:BaseViewModel
    {
        [Display(Name = "Серийный №")]
        [RegularExpression(@"^[0-9]+$",ErrorMessage = "Неверный формат")]
        [Remote("IsSerialNumProductExist","Products",
            ErrorMessage = "Серийный номер уже существует",
            AdditionalFields = "Id")]
        public string SerialNum { get; set; }
        [Display(Name = "Зав. №")]
        [RegularExpression(@"^[0-9]+$",ErrorMessage = "Неверный формат")]
        [Remote("IsCertifiedNumProductExist","Products",
            ErrorMessage = "Серийный номер уже существует",
            AdditionalFields = "Id")]
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
        
        [Display(Name = "Дата Заказа")]
        public DateTime? OrderDate { get; set; }// дата изготовления
        [Display(Name = "Дата изготовления")]
        public DateTime? ManufacturingDate { get; set; }// дата изготовления
        [Display(Name = "Дата отгрузки")]
        public DateTime? ShippedDate { get; set; }// дата отгрузки

        /*// поля со значения для выпадающих списков
        public SelectList Subprojects { get; set; }
        
        public SelectList ProductTypes { get; set; }
        */
    }
}