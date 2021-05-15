using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class OrderEmployee
    {
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public int OrderCardId { get; set; }
        public virtual OrderCard OrderCard { get; set; }
        public int StatusParticipationId { get; set; }
        public virtual Status StatusParticipation { get; set; }
    }
}
