using System.Collections.Generic;

namespace Cursa.ViewModels.OrderCardVM
{
    public class OrderCardSummaryModule
    {
        public string ModuleTypeName { get; set; }
        public IEnumerable<int> Addresses { get; set; }
    }
}