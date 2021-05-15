using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    class ModuleCharacteristic
    {
        public int Id { get; set; }
        public int CharacteristicId { get; set; }
        public virtual Characteristic Characteristic { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
