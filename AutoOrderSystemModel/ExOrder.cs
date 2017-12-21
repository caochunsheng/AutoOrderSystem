using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoOrderSystem.Model
{
    public class ExOrder
    {
        public string OrderNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Remarks { get; set; }
        public List<ExOrderItem> ItemList { get; set; }
    }
}
