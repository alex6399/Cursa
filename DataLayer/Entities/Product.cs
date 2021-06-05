﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class Product:BaseEntityTracking
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Наименование")]
        [MaxLength(51, ErrorMessage = "Максимальное количество символов 51")]
        public string Name { get; set; }
        [Display(Name = "Серийный номер")]
        [MaxLength(51, ErrorMessage = "Максимальное количество символов 51")]
        public string SerialNum { get; set; }
        [Display(Name = "Зав. номер")]
        [MaxLength(51, ErrorMessage = "Максимальное количество символов 51")]
        public string CertifiedNum { get; set; }
        [Display(Name = "Тип")]
        public int ProductTypeId { get; set; }
        [Display(Name = "Тип")]
        public virtual ProductType ProductType { get; set; }
        [Display(Name = "Подпроект")]
        public int SubProjectId { get; set; }
        [Display(Name = "Подпроект")]
        public SubProject SubProject { get; set; }
        [Display(Name = "Сформирован")]
        public bool IsFormed { get; set; } // сформирован?
        [Display(Name = "Дата заказа")]
        public DateTime? OrderDate { get; set; }
        [Display(Name = "Дата изготовления")]
        public DateTime? ManufacturingDate { get; set; }// дата изготовления
        [Display(Name = "Дата отгрузки")]
        public DateTime? ShippedDate { get; set; }// дата отгрузки
        [Display(Name = "Описание")]
        [MaxLength(160,ErrorMessage = "Допустимо не более 160 символов")]
        public string Description { get; set; }

        public ICollection<OrderCard> OrderCards { get; set; }
    }
}