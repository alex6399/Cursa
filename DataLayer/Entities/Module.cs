using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities.Bases;

namespace DataLayer.Entities
{
    public class Module:BaseEntityTracking
    {
        public int OrderCardId { get; set; }
        public virtual OrderCard OrderCard { get; set; }
        public int? SerialNumModule { get; set; }
        public int Place { get; set; }
        public bool IsInstalled { get; set; }
        public DateTime? ManufacturingData { get; set; }
    }
}
// TODO как сохранять вложенные сущности
// добавить отдельную сущность характеристика, в которой будет ключ на Модуль,
// но выводить в одной ViewModel